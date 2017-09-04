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
//using System.ComponentModel.Design;
//using System.Runtime.InteropServices;
using EnvDTE;
//using Microsoft.VisualStudio.Shell;
//using Microsoft.VisualStudio.Shell.Interop;
using Trivial.Entities;
using Trivial.Ui.Common;
//////////////////////////////////////////////////using Package = Microsoft.VisualStudio.Shell.Package;

namespace Trivial.Ui.NumericTrivia
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: Vsix.Version, IconResourceID = 400)]//[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(Vsix.Id)]//[Guid(VSPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class VSPackage : Package
    {
        private DTE dte;

        ///////////public const string PackageGuidString = "03e50d6d-c0f6-4d7c-b645-39f2c0395329";

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

        private void OnSolutionOpened()
        {
            TriviaHelper.ShowTrivia(AppName.NumericTrivia, Vsix.Name);
        }
    }
}