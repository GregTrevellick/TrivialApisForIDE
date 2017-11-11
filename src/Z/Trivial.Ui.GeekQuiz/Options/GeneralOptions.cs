﻿using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using Trivial.Ui.Common;

namespace Trivial.Ui.GeekQuiz.Options
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
            get => string.IsNullOrEmpty(timeOutInMilliSeconds) ? CommonConstants.DefaultTimeOutInMilliSeconds : timeOutInMilliSeconds;
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

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.ShowTriviaUponClosingSolutionOptionLabel)]
        [Description(CommonConstants.ShowTriviaUponClosingSolutionOptionDetailedDescription)]
        public bool ShowTriviaUponClosingSolution { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.ShowTriviaUponOpeningSolutionOptionLabel)]
        [Description(CommonConstants.ShowTriviaUponOpeningSolutionOptionDetailedDescription)]
        public bool ShowTriviaUponOpeningSolution { get; set; } = true;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.SuppressClosingWithoutSubmitingAnswerWarningOptionLabel)]
        [Description(CommonConstants.SuppressClosingWithoutSubmitingAnswerWarningOptionDetailedDescription)]
        public bool SuppressClosingWithoutSubmitingAnswerWarning { get; set; } = false;
    }
}