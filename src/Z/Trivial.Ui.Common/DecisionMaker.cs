using System;

namespace Trivial.Ui.Common
{
    public class DecisionMaker
    {
        public bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            var dateTimeNow = DateTime.Now;

            if (!HaveExceededTodaysPopUpCount(generalOptionsDto, dateTimeNow))
            {
                if (LastPopUpMoreThanXMinutesAgo(generalOptionsDto.LastPopUpDateTime, generalOptionsDto.PopUpIntervalInMins, dateTimeNow))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HaveExceededTodaysPopUpCount(GeneralOptionsDto generalOptionsDto, DateTime dateTimeNow)
        {
            var isWeekend = IsWeekend(dateTimeNow);
            bool haveExceededTodaysPopUpCount;

            if (isWeekend)
            {
                haveExceededTodaysPopUpCount = HaveExceededTodaysPopUpCount(generalOptionsDto.PopUpCountToday, generalOptionsDto.MaximumPopUpsWeekEnd);
            }
            else
            {
                haveExceededTodaysPopUpCount = HaveExceededTodaysPopUpCount(generalOptionsDto.PopUpCountToday, generalOptionsDto.MaximumPopUpsWeekDay);
            }

            return haveExceededTodaysPopUpCount;
        }

        private bool IsWeekend(DateTime dateTimeNow)
        {
            return dateTimeNow.DayOfWeek == DayOfWeek.Saturday ||
                   dateTimeNow.DayOfWeek == DayOfWeek.Sunday;
        }

        internal bool HaveExceededTodaysPopUpCount(int popUpCountToday, int maximumPopUps)
        {
            return popUpCountToday >= maximumPopUps;
        }

        internal bool LastPopUpMoreThanXMinutesAgo(DateTime lastPopUpDateTime, int popUpIntervalInMins, DateTime dateTimeNow)
        {
            var acceptableLastPopUpDateTime = dateTimeNow.AddMinutes(-1 * popUpIntervalInMins);
            var result = lastPopUpDateTime <= acceptableLastPopUpDateTime;
            return result;
        }
    }
}
