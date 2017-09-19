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
                if (LastPopUpMoreThanXMinutesAgo(generalOptionsDto, DateTime.Now))
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool MidweekAndHaveNotExceededMidweekCount(GeneralOptionsDto generalOptionsDto, bool isWeekend)
        {
            return 
                !isWeekend &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay;
        }

        private static bool IsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || 
                   dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        internal static bool WeekEndAndHaveNotExceededWeekEndCount(GeneralOptionsDto generalOptionsDto, bool isWeekend)//gregtlo unit test reqd
        {
            return 
                isWeekend && 
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd;
        }

        internal static bool LastPopUpMoreThanXMinutesAgo(GeneralOptionsDto generalOptionsDto, DateTime dateTime)//gregtlo unit test reqd
        {
            return generalOptionsDto.LastPopUpDateTime < dateTime.AddMinutes(-1 * generalOptionsDto.PopUpIntervalInMins);
        }
    }
}
