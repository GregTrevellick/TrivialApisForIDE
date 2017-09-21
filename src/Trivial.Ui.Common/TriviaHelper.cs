using System;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            if (!PopUpLimitForTodayExceeded(generalOptionsDto))
            {
                if (LastPopUpMoreThanXMinutesAgo(generalOptionsDto.LastPopUpDateTime, generalOptionsDto.PopUpIntervalInMins, DateTime.Now))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool PopUpLimitForTodayExceeded(GeneralOptionsDto generalOptionsDto)
        {
            var isWeekend = IsWeekend(DateTime.Now);

            var result = 
                WeekEndAndHaveNotExceededWeekEndCount(generalOptionsDto, isWeekend) || 
                MidweekAndHaveNotExceededMidweekCount(generalOptionsDto, isWeekend);

            return result;
        }

        private static bool IsWeekend(DateTime dateTimeNow)
        {
            return dateTimeNow.DayOfWeek == DayOfWeek.Saturday ||
                   dateTimeNow.DayOfWeek == DayOfWeek.Sunday;
        }

        internal static bool MidweekAndHaveNotExceededMidweekCount(GeneralOptionsDto generalOptionsDto, bool isWeekend)
        {
            var result = 
                !isWeekend &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay;

            return result;
        }

        internal static bool WeekEndAndHaveNotExceededWeekEndCount(GeneralOptionsDto generalOptionsDto, bool isWeekend)
        {
            var result = 
                isWeekend && 
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd;

            return result;
        }

        internal static bool LastPopUpMoreThanXMinutesAgo(DateTime lastPopUpDateTime, int popUpIntervalInMins, DateTime dateTimeNow)
        {
            return lastPopUpDateTime < dateTimeNow.AddMinutes(-1 * popUpIntervalInMins);
        }
    }
}
