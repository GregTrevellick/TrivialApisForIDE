//gregt future - options 
//03 specify web service timeout
//50 add app icon to pop up 
//97 choice of open ide or open sln
//98 choice of model dialog or simple dialog
//99 font size

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Trivial.Ui.Common;

namespace Trivial.Ui.TrumpQuotes.Options
{
    public class GeneralOptions : DialogPage
    {
        internal DateTime LastPopUpDateTime { get; set; } = DateTime.Now;
        internal int PopUpCountToday { get; set; }
        private string maximumPopUpsWeekDay;
        private string maximumPopUpsWeekEnd;
        private string popUpIntervalInMins;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Maximum pop ups Mon-Fri day")]
        [Description("Maximum pop ups Mon-Fri descr.")]
        public int MaximumPopUpsWeekDay { get; set; } = 3;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Maximum pop ups Sat-Sun day")]
        [Description("Maximum pop ups Sat-Sun descr.")]
        public int MaximumPopUpsWeekEnd { get; set; } = int.MaxValue;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.PopUpIntervalInMinsOptionLabel)]
        [Description(CommonConstants.PopUpIntervalInMinsOptionDetailedDescription)]
        public string PopUpIntervalInMins
        {
            get
            {
                if (string.IsNullOrEmpty(popUpIntervalInMins))
                {
                    return CommonConstants.DefaultFileQuantityWarningLimit;
                }
                else
                {
                    return popUpIntervalInMins;
                }
            }
            set
            {
                var isInteger = int.TryParse(value, out int x);
                if (isInteger)
                {
                    popUpIntervalInMins = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.PopUpIntervalInMinsOptionLabel);
                }
            }
        }

        private static void DisplayInvalidIntegerError(string labelName)
        {
            var constantsForAppCommon = new ConstantsForAppCommon();
            MessageBox.Show(
                constantsForAppCommon.GetInvalidInteger(labelName),
                constantsForAppCommon.Caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        internal int PopUpIntervalInMinsInt
        {
            get
            {
                var isInteger = int.TryParse(PopUpIntervalInMins, out int x);
                return isInteger ? x : 0;
            }
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            VSPackage.Options.SaveSettingsToStorage();
            base.OnApply(e);
        }
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////public override void LoadSettingsFromStorage()
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    base.LoadSettingsFromStorage();
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
