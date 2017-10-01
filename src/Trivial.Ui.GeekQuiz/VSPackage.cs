using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Trivial.Entities;
using Trivial.Ui.GeekQuiz;
using Trivial.Ui.GeekQuiz.Options;
using Trivial.Ui.Common;
//using Trivial.Ui.Common.Ratings;
//using VsixRatingChaser;

namespace Trivial.Ui.GeekQuiz
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)]
    [Guid(Vsix.Id)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, CommonConstants.CategorySubLevel, 0, 0, true)]
    public sealed class VSPackage : Package
    {
        private DTE dte;
        private SolutionEvents solutionEvents;

        protected override void Initialize()
        {
            base.Initialize();

            IServiceContainer serviceContainer = this as IServiceContainer;
            dte = serviceContainer.GetService(typeof(SDTE)) as DTE;
            solutionEvents = dte.Events.SolutionEvents;
            solutionEvents.Opened += OnSolutionOpened;
        }

        private void OnSolutionOpened()
        {
            //ChaseRatings();

            var shouldShowTrivia = new DecisionMaker().ShouldShowTrivia(GeneralOptionsDto);

            if (shouldShowTrivia)
            {
                var popUpTitle = CommonConstants.GetCaption(Vsix.Name, Vsix.Version);
                var triviaMessage = new TriviaMessage();
                var hiddenOptionsDto = triviaMessage.ShowTrivia(AppName.GeekQuiz, popUpTitle, 
                    GeneralOptionsDto.LastPopUpDateTime, GeneralOptionsDto.PopUpCountToday, GeneralOptionsDto.TimeOutInMilliSeconds, Vsix.Name);

                if (hiddenOptionsDto != null)
                {
                    UpdateHiddenOptions(hiddenOptionsDto);
                }
            }
        }

        //private void ChaseRatings()
        //{
        //    var hiddenRatingChaserOptions = (IHiddenRatingChaserOptions) GetDialogPage(typeof(HiddenRatingChaserOptions));
        //    var ratingInstructionsDto = new RatingInstructionsDto
        //    {
        //        PackageLoadedLimit = CommonConstants.PackageLoadedLimit,
        //        RatingRequestLimit = CommonConstants.RatingRequestLimit,
        //    };
        //    RatingChaser.ChaseRatings(hiddenRatingChaserOptions, ratingInstructionsDto);
        //}

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
    }
}