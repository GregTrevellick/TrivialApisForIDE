using System;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            if (WeekEndAndHaveNotExceededWeekEndCount(generalOptionsDto) || 
                MidweekAndHaveNotExceededMidweekCount(generalOptionsDto))
            {
                if (LastPopUpMoreThanXMinutesAgo(generalOptionsDto))
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool MidweekAndHaveNotExceededMidweekCount(GeneralOptionsDto generalOptionsDto)//gregtlo unit test reqd
        {
            return 
                DateTime.Now.DayOfWeek != DayOfWeek.Saturday && 
                DateTime.Now.DayOfWeek != DayOfWeek.Sunday &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay;
        }

        private static bool WeekEndAndHaveNotExceededWeekEndCount(GeneralOptionsDto generalOptionsDto)//gregtlo unit test reqd
        {
            return (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) &&
                    generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd;
        }

        private static bool LastPopUpMoreThanXMinutesAgo(GeneralOptionsDto generalOptionsDto)//gregtlo unit test reqd
        {
            return generalOptionsDto.LastPopUpDateTime < DateTime.Now.AddMinutes(-1 * generalOptionsDto.PopUpIntervalInMins);
        }
    }
}
