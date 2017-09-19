using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Trivial.Entities;
using Trivial.Ui.Common;
//////////////////////////////////////////////////using Package = Microsoft.VisualStudio.Shell.Package;

namespace Trivial.Ui.NumericTrivia
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)]
    [ProvideOptionPage(typeof(OptionsDialogPage), Vsix.Name, "General", 0, 0, supportsAutomation: true)]
    [Guid(Vsix.Id)]
    public sealed class VSPackage : Package
    {
        private DTE dte;

        public VSPackage()
        {
            //gregt call the web service here for performance sake ?
        }

        protected override void Initialize()
        {
            base.Initialize();

            IServiceContainer serviceContainer = this as IServiceContainer;
            dte = serviceContainer.GetService(typeof(SDTE)) as DTE;
            var solutionEvents = dte.Events.SolutionEvents;
            solutionEvents.Opened += OnSolutionOpened;
        }

        private void OnSolutionOpened()
        {
            TriviaHelper.ShowTrivia(AppName.NumericTrivia, Vsix.Name);
        }
    }
}