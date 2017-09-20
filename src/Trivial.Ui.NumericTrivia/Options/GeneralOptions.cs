using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Trivial.Ui.Common;

namespace Trivial.Ui.NumericTrivia.Options
{
    public class GeneralOptions : DialogPage
    {
        internal DateTime LastPopUpDateTime { get; set; } = DateTime.Now;
        internal int PopUpCountToday { get; set; }
        private string maximumPopUpsWeekDay;
        private string maximumPopUpsWeekEnd;
        private string popUpIntervalInMins;
        private bool proceedToSaveOptions;
        private string timeOutInMilliSeconds;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.MaximumPopUpsWeekDayOptionLabel)]
        [Description(CommonConstants.MaximumPopUpsWeekDayDetailedDescription)]
        public string MaximumPopUpsWeekDay 
        {
            get => string.IsNullOrEmpty(maximumPopUpsWeekDay) ? CommonConstants.DefaultMaximumPopUpsWeekDay : maximumPopUpsWeekDay;
            set
            {
                if (value.IsNonNegativeInteger())
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
                if (value.IsNonNegativeInteger())
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
                if (value.IsNonNegativeInteger())
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
                if (value.IsNonNegativeInteger())
                {
                    if (value.IsWithinRecommendedTimeoutLimits())
                    {
                        timeOutInMilliSeconds = value;
                    }
                    else
                    {
                        if (!proceedToSaveOptions)
                        {
                            proceedToSaveOptions = ConfirmProceedToSaveTimeOutInMilliSeconds();
                            if (proceedToSaveOptions)
                            {
                                timeOutInMilliSeconds = value;
                            }
                        }
                    }
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

        private static bool ConfirmProceedToSaveTimeOutInMilliSeconds()
        {
            var proceedToSave = false;

            var box = MessageBox.Show(
                CommonConstants.TimeOutInMilliSecondsIsOutsideRecommendedTimeoutLimits,
                CommonConstants.GetCaption(Vsix.Name, Vsix.Version),
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (box == DialogResult.OK)
            {
                proceedToSave = true;
            }

            return proceedToSave;
        }
    }
}