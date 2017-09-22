using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using Trivial.Ui.Common;

namespace Trivial.Ui.NumericTrivia.Options
{
    public class GeneralOptions : DialogPage
    {
        private string caption = CommonConstants.GetCaption(Vsix.Name, Vsix.Version);
        private bool firstTimeOpeningOptions = true;
        private string maximumPopUpsWeekDay;
        private string maximumPopUpsWeekEnd;
        private string popUpIntervalInMins;
        private bool proceedToSaveOptions;
        private string timeOutInMilliSeconds;

        /////////////////////////[Category(CommonConstants.AppInternal)]
        /////////////////////////[DisplayName(CommonConstants.LastPopUpDateTimeLabel)]
        /////////////////////////[Description(CommonConstants.LastPopUpDateTimeDescription)]
        public DateTime LastPopUpDateTime { get; set; }

        /////////////////////////[Category(CommonConstants.AppInternal)]
        /////////////////////////[DisplayName(CommonConstants.PopUpCountTodayLabel)]
        /////////////////////////[Description(CommonConstants.PopUpCountTodayDescription)]
        public int PopUpCountToday { get; set; }

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
                    MessageBoxes.DisplayInvalidIntegerError(CommonConstants.MaximumPopUpsWeekDayOptionLabel, caption);
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
                    MessageBoxes.DisplayInvalidIntegerError(CommonConstants.MaximumPopUpsWeekEndOptionLabel, caption);
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
                    MessageBoxes.DisplayInvalidIntegerError(CommonConstants.PopUpIntervalInMinsOptionLabel, caption);
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
                        if (proceedToSaveOptions)
                        {
                            timeOutInMilliSeconds = value;
                        }
                        else
                        {
                            if (firstTimeOpeningOptions)
                            {
                                firstTimeOpeningOptions = false;
                                timeOutInMilliSeconds = value;
                            }
                            else
                            {
                                proceedToSaveOptions = MessageBoxes.ConfirmProceedToSaveTimeOutInMilliSeconds(caption);
                                if (proceedToSaveOptions)
                                {
                                    timeOutInMilliSeconds = value;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBoxes.DisplayInvalidIntegerError(CommonConstants.TimeOutInMilliSecondsOptionLabel, caption);
                }
            }
        }
    }
}