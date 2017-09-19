using System;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            var isWeekend = IsWeekend(DateTime.Now);

            if (WeekEndAndHaveNotExceededWeekEndCount(generalOptionsDto, isWeekend) || 
                MidweekAndHaveNotExceededMidweekCount(generalOptionsDto, isWeekend))
            {
                if (LastPopUpMoreThanXMinutesAgo(generalOptionsDto.LastPopUpDateTime, generalOptionsDto.PopUpIntervalInMins, DateTime.Now))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsWeekend(DateTime dateTimeNow)
        {
            return dateTimeNow.DayOfWeek == DayOfWeek.Saturday ||
                   dateTimeNow.DayOfWeek == DayOfWeek.Sunday;
        }

        internal static bool MidweekAndHaveNotExceededMidweekCount(GeneralOptionsDto generalOptionsDto, bool isWeekend)
        {
            return 
                !isWeekend &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay;
        }

        internal static bool WeekEndAndHaveNotExceededWeekEndCount(GeneralOptionsDto generalOptionsDto, bool isWeekend)
        {
            return 
                isWeekend && 
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd;
        }

        internal static bool LastPopUpMoreThanXMinutesAgo(DateTime lastPopUpDateTime, int popUpIntervalInMins, DateTime dateTimeNow)
        {
            return lastPopUpDateTime < dateTimeNow.AddMinutes(-1 * popUpIntervalInMins);
        }
    }
}
