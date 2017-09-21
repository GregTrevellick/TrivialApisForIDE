using System;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
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

        private static bool HaveExceededTodaysPopUpCount(GeneralOptionsDto generalOptionsDto, DateTime dateTimeNow)
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

        private static bool IsWeekend(DateTime dateTimeNow)
        {
            return dateTimeNow.DayOfWeek == DayOfWeek.Saturday ||
                   dateTimeNow.DayOfWeek == DayOfWeek.Sunday;
        }

        internal static bool HaveExceededTodaysPopUpCount(int popUpCountToday, int maximumPopUps)
        {
            return popUpCountToday >= maximumPopUps;
        }

        internal static bool LastPopUpMoreThanXMinutesAgo(DateTime lastPopUpDateTime, int popUpIntervalInMins, DateTime dateTimeNow)
        {
            var acceptableLastPopUpDateTime = dateTimeNow.AddMinutes(-1 * popUpIntervalInMins);
            var result = lastPopUpDateTime <= acceptableLastPopUpDateTime;
            return result;
        }
    }
}
