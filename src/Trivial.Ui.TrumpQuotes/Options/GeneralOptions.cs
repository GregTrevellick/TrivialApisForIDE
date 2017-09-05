using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Trivial.Ui.Common;

namespace Trivial.Ui.TrumpQuotes.Options
{
    public class GeneralOptions : DialogPage
    {
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("FrequencyInterval")]
        [Description("FrequencyInterval descr.")]
        public int FrequencyInterval { get; set; } = 1;

        //[Category(CommonConstants.CategorySubLevel)]
        //[DisplayName("Show source file names")]
        //[Description("Show or hide the file names of the files containing content that appears in the pop-up window.")]
        //public bool ShowFileNamesInPopUp { get; set; } = true;



        //gregt future - options 
        //02 frequency of delivery
        //03 specify web service timeout
        //50 add app icon to pop up 
        //97 choice of open ide or open sln
        //98 choice of model dialog or simple dialog
        //99 font size




        //gregt to be deleted
        //[Category(CommonConstants.CategorySubLevel)]
        //[DisplayName(CommonConstants.FileQuantityWarningLimitOptionLabel)]
        //[Description(CommonConstants.FileQuantityWarningLimitOptionDetailedDescription)]
        //public string FileQuantityWarningLimit
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(fileQuantityWarningLimit))
        //        {
        //            return CommonConstants.DefaultFileQuantityWarningLimit;
        //        }
        //        else
        //        {
        //            return fileQuantityWarningLimit;
        //        }
        //    }
        //    set
        //    {
        //        int x;
        //        var isInteger = int.TryParse(value, out x);
        //        if (!isInteger)
        //        {
        //            MessageBox.Show(
        //                CommonConstants.FileQuantityWarningLimitInvalid,
        //                new ConstantsForAppCommon().Caption,
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);
        //        }
        //        else
        //        {
        //            fileQuantityWarningLimit = value;
        //        }
        //    }
        //}

        //private string fileQuantityWarningLimit;
        //private string typicalFileExtensions;

        //internal int FileQuantityWarningLimitInt
        //{
        //    get
        //    {
        //        int x;
        //        var isInteger = int.TryParse(FileQuantityWarningLimit, out x);
        //        if (isInteger)
        //        {
        //            return x;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        //public override void LoadSettingsFromStorage()
        //{
        //    base.LoadSettingsFromStorage();

        //    //if (string.IsNullOrEmpty(TypicalFileExtensions))
        //    //{
        //    //    TypicalFileExtensions = AllAppsHelper.GetDefaultTypicalFileExtensionsAsCsv(defaultTypicalFileExtensions);
        //    //}

        //    //if (string.IsNullOrEmpty(ActualPathToExe))
        //    //{
        //    //    ActualPathToExe = GeneralOptionsHelper.GetActualPathToExe(keyToExecutableEnum);
        //    //}

        //    //previousActualPathToExe = ActualPathToExe;
        //}

        //private string previousActualPathToExe { get; set; }

        //protected override void OnApply(PageApplyEventArgs e)
        //{
        //    //var actualPathToExeChanged = false;

        //    //if (ActualPathToExe != previousActualPathToExe)
        //    //{
        //    //    actualPathToExeChanged = true;
        //    //    previousActualPathToExe = ActualPathToExe; 
        //    //}

        //    //if (actualPathToExeChanged)
        //    //{
        //    //    if (!ArtefactsHelper.DoesActualPathToExeExist(ActualPathToExe))
        //    //    {
        //    //        e.ApplyBehavior = ApplyKind.Cancel;

        //    //        var caption = new ConstantsForAppCommon().Caption;

        //    //        var filePrompterHelper = new FilePrompterHelper(caption, keyToExecutableEnum.Description());

        //    //        var persistOptionsDto = filePrompterHelper.PromptForActualExeFile(ActualPathToExe);

        //    //        if (persistOptionsDto.Persist)
        //    //        {
        //    //            PersistVSToolOptions(persistOptionsDto.ValueToPersist);
        //    //        }
        //    //    }
        //    //}

        //    base.OnApply(e);
        //}

        //public void PersistVSToolOptions(string fileName)
        //{
        //    VSPackage.Options.ActualPathToExe = fileName;
        //    VSPackage.Options.SaveSettingsToStorage();
        //}
    }
}