using System;
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
        private string timeOutInMilliSeconds;//gregthi is this option being used properly ?

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.MaximumPopUpsWeekDayOptionLabel)]
        [Description(CommonConstants.MaximumPopUpsWeekDayDetailedDescription)]
        public string MaximumPopUpsWeekDay 
        {
            get => string.IsNullOrEmpty(maximumPopUpsWeekDay) ? CommonConstants.DefaultMaximumPopUpsWeekDay : maximumPopUpsWeekDay;
            set
            {
                if (value.IsInteger())
                {
                    maximumPopUpsWeekDay = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.MaximumPopUpsWeekDayOptionLabel);
                }
            }
        }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.MaximumPopUpsWeekEndOptionLabel)]
        [Description(CommonConstants.MaximumPopUpsWeekEndDetailedDescription)]
        public string MaximumPopUpsWeekEnd 
        {
            get => string.IsNullOrEmpty(maximumPopUpsWeekEnd) ? CommonConstants.DefaultMaximumPopUpsWeekEnd : maximumPopUpsWeekEnd;
            set
            {
                if (value.IsInteger())
                {
                    maximumPopUpsWeekEnd = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.MaximumPopUpsWeekEndOptionLabel);
                }
            }
        }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.PopUpIntervalInMinsOptionLabel)]
        [Description(CommonConstants.PopUpIntervalInMinsOptionDetailedDescription)]
        public string PopUpIntervalInMins
        {
            get => string.IsNullOrEmpty(popUpIntervalInMins) ? CommonConstants.DefaultPopUpIntervalInMins : popUpIntervalInMins;
            set
            {
                if (value.IsInteger())
                {
                    popUpIntervalInMins = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.PopUpIntervalInMinsOptionLabel);
                }
            }
        }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.TimeOutInMilliSecondsOptionLabel)]
        [Description(CommonConstants.TimeOutInMilliSecondsOptionDetailedDescription)]
        public string TimeOutInMilliSeconds
        {
            get => string.IsNullOrEmpty(timeOutInMilliSeconds) ? CommonConstants.DefaultPopUpIntervalInMins : timeOutInMilliSeconds;
            set
            {
                if (value.IsInteger())
                {
                    timeOutInMilliSeconds = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.TimeOutInMilliSecondsOptionLabel);
                }
            }
        }

        private static void DisplayInvalidIntegerError(string labelName)
        {
            MessageBox.Show(
                CommonConstants.GetInvalidInteger(labelName),
                CommonConstants.GetCaption(Vsix.Name, Vsix.Version),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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
