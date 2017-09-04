using EnvDTE;
//using System;
using System.ComponentModel.Design;
//using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
//using System.Globalization;
using System.Runtime.InteropServices;
//using Microsoft.VisualStudio;
//using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
//using Microsoft.Win32;
using Trivial.Entities;
using Trivial.Ui.Common;

namespace Trivial.Ui.TrumpQuotes
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)]
    [Guid(Vsix.Id)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    //TODO GREGT [ProvideOptionPage(typeof(OptionsDialogPage), Vsix.Name, "General", 0, 0, supportsAutomation: true)]
    public sealed class VSPackage : Package
    {
        private DTE dte;

        public VSPackage()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            IServiceContainer serviceContainer = this as IServiceContainer;
            dte = serviceContainer.GetService(typeof(SDTE)) as DTE;
            var solutionEvents = dte.Events.SolutionEvents;
            solutionEvents.Opened += OnSolutionOpened;
        }

        private static void OnSolutionOpened()
        {
            TriviaHelper.ShowTrivia(AppName.TrumpQuotes, Vsix.Name);
        }
    }
}
