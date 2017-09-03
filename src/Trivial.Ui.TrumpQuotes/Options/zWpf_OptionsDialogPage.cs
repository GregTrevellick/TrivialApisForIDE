using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
//using System.Windows;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
//using Trivial.Ui.TrumpQuotes;

//using Microsoft.VisualStudio.ComponentModelHost;

//gregt future - options 
//01 choice of open ide or open sln
//02 frequency of delivery
//03 specify web service timeout
//50 add app icon to pop up 
//98 choice of model dialog or simple dialog
//99 font size

namespace Trivial.Ui.TrumpQuotes
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false)]
    [ComVisible(true)]
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////[Guid("1D9ECCF3-5D2F-4112-9B25-264596873DC9")]
    public class OptionsDialogPage : UIElementDialogPage
    {
        OptionsDialogPageControl optionsDialogPageControl;

        protected override System.Windows.UIElement Child
        {
            get
            {
                OptionsDialogPageControl result = this.optionsDialogPageControl ?? (this.optionsDialogPageControl = new OptionsDialogPageControl());
                return result;
            }
        }

       /// protected override System.Windows.UIElement Child => throw new NotImplementedException();

        protected override void OnActivate(CancelEventArgs e)
        {
            base.OnActivate(e);
            /////////////////////////var a = optionsDialogPageControl.CheckBoxIde;
            /////////////////////////var b = optionsDialogPageControl.CheckBoxSln;
            //    var encouragements = GetEncouragements();
            //    optionsDialogControl.Encouragements = string.Join(Environment.NewLine, encouragements.AllEncouragements);
        }

        protected override void OnApply(PageApplyEventArgs args)
        {
            //    if (args.ApplyBehavior == ApplyKind.Apply)
            //    {
            //        string[] userEncouragments = optionsDialogControl.Encouragements.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //        GetEncouragements().AllEncouragements = userEncouragments;
            //    }

            base.OnApply(args);
        }

        //IEncouragements GetEncouragements()
        //{
        //    var componentModel = (IComponentModel)(Site.GetService(typeof(SComponentModel)));
        //    return componentModel.DefaultExportProvider.GetExportedValue<IEncouragements>();
        //}
    }
}
