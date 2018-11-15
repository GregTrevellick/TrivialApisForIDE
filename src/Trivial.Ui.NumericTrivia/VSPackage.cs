using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Trivial.Entities;
using Trivial.Ui.Common;
using Trivial.Ui.NumericTrivia.Options;
using SolutionEvents = Microsoft.VisualStudio.Shell.Events.SolutionEvents;
using Task = System.Threading.Tasks.Task;

namespace Trivial.Ui.NumericTrivia
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)]
    [Guid(Vsix.Id)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, CommonConstants.CategorySubLevel, 0, 0, true)]
    public sealed class VSPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            bool isSolutionLoaded = await IsSolutionLoadedAsync();

            if (isSolutionLoaded)
            {
                await HandleOpenSolutionAsync();
            }

            SolutionEvents.OnAfterOpenSolution += HandleOpenSolution;
        }

        private async Task<bool> IsSolutionLoadedAsync()
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync();

            var solService = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            ErrorHandler.ThrowOnFailure(solService.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));

            return value is bool isSolOpen && isSolOpen;
        }

        private void HandleOpenSolution(object sender = null, EventArgs e = null)
        {
            Task.Run(async () => await HandleOpenSolutionAsync());
        }

        private async Task HandleOpenSolutionAsync(object sender = null, EventArgs e = null)
        {
            var shouldShowTrivia = new DecisionMaker().ShouldShowTrivia(GeneralOptionsDto);

            if (shouldShowTrivia)
            {
                var popUpTitle = CommonConstants.GetCaption(Vsix.Name, Vsix.Version);
                var hiddenOptionsDto = new TriviaMessage().ShowTrivia(AppName.NumericTrivia, popUpTitle, GeneralOptionsDto.LastPopUpDateTime, GeneralOptionsDto.PopUpCountToday, GeneralOptionsDto.TimeOutInMilliSeconds, Vsix.Name);

                if (hiddenOptionsDto != null)
                {
                    UpdateHiddenOptions(hiddenOptionsDto);
                }

                //ChaseRating();
            }
        }

        private void UpdateHiddenOptions(HiddenOptionsDto hiddenOptionsDto)
        {
            var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
            hiddenOptions.LastPopUpDateTime = hiddenOptionsDto.LastPopUpDateTime;
            hiddenOptions.PopUpCountToday = hiddenOptionsDto.PopUpCountToday;
            hiddenOptions.SaveSettingsToStorage();
        }

        private GeneralOptionsDto GeneralOptionsDto
        {
            get
            {
                var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));
                var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));

                return new GeneralOptionsDto
                {
                    LastPopUpDateTime = hiddenOptions.LastPopUpDateTime,
                    MaximumPopUpsWeekDay = generalOptions.MaximumPopUpsWeekDay.GetAsInteger(),
                    MaximumPopUpsWeekEnd = generalOptions.MaximumPopUpsWeekEnd.GetAsInteger(),
                    PopUpIntervalInMins = generalOptions.PopUpIntervalInMins.GetAsInteger(),
                    PopUpCountToday = hiddenOptions.PopUpCountToday,
                    TimeOutInMilliSeconds = generalOptions.TimeOutInMilliSeconds.GetAsInteger(),
                };
            }
        }

        //private void ChaseRating()
        //{
        //    var hiddenChaserOptions = (IRatingDetailsDto)GetDialogPage(typeof(HiddenRatingDetailsDto));
        //    var packageRatingChaser = new PackageRatingChaser();
        //    packageRatingChaser.Hunt(hiddenChaserOptions);
        //}

    }
}