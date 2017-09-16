﻿using EnvDTE;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using Trivial.Entities;
using Trivial.Ui.Common;
using Trivial.Ui.TrumpQuotes.Options;

namespace Trivial.Ui.TrumpQuotes
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
        public static GeneralOptions Options { get; private set; }

        public VSPackage()
        {
            //gregt call the web service here for performance sake ?
        }

        protected override void Initialize()
        {
            Options = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));

            base.Initialize();

            IServiceContainer serviceContainer = this as IServiceContainer;
            dte = serviceContainer.GetService(typeof(SDTE)) as DTE;
            var solutionEvents = dte.Events.SolutionEvents;
            solutionEvents.Opened += OnSolutionOpened;
        }

        private void OnSolutionOpened()
        {
            var shouldShowTrivia = TriviaHelper.ShouldShowTrivia(GeneralOptionsDto);

            if (shouldShowTrivia)
            {
                var hiddenOptionsDto = TriviaHelper.ShowTrivia(AppName.TrumpQuotes, Vsix.Name, GeneralOptionsDto.LastPopUpDateTime, GeneralOptionsDto.PopUpCountToday, GeneralOptionsDto.TimeOutInMilliSeconds);

                if (hiddenOptionsDto != null)
                {
                    UpdateHiddenOptions(hiddenOptionsDto);
                }
            }
        }

        private static void UpdateHiddenOptions(HiddenOptionsDto hiddenOptionsDto)
        {
            Options.LastPopUpDateTime = hiddenOptionsDto.LastPopUpDateTime;
            Options.PopUpCountToday = hiddenOptionsDto.PopUpCountToday;
            Options.SaveSettingsToStorage();
        }

        private GeneralOptionsDto GeneralOptionsDto
        {
            get
            {
                var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));

                return new GeneralOptionsDto
                {
                    LastPopUpDateTime = generalOptions.LastPopUpDateTime,
                    MaximumPopUpsWeekDay = generalOptions.MaximumPopUpsWeekDayInt,
                    MaximumPopUpsWeekEnd = generalOptions.MaximumPopUpsWeekEndInt,
                    PopUpIntervalInMins = generalOptions.PopUpIntervalInMinsInt,
                    PopUpCountToday = generalOptions.PopUpCountToday,
                    TimeOutInMilliSeconds = 1000,
                };
            }
        }
    }
}
