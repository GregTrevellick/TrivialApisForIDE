using System;
using Trivial.Api.Gateway;
using Trivial.Entities;

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

        private static bool MidweekAndHaveNotExceededMidweekCount(GeneralOptionsDto generalOptionsDto)//gregt unit test reqd

        {
            return 
                DateTime.Now.DayOfWeek != DayOfWeek.Saturday && 
                DateTime.Now.DayOfWeek != DayOfWeek.Sunday &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay;
        }

        private static bool WeekEndAndHaveNotExceededWeekEndCount(GeneralOptionsDto generalOptionsDto)//gregt unit test reqd

        {
            return (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) &&
                    generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd;
        }

        private static bool LastPopUpMoreThanXMinutesAgo(GeneralOptionsDto generalOptionsDto)//gregt unit test reqd
        {
            return generalOptionsDto.LastPopUpDateTime < DateTime.Now.AddMinutes(-1 * generalOptionsDto.PopUpIntervalInMins);
        }

        public static HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday)
        {
            HiddenOptionsDto hiddenOptionsDto = null; 

            var gatewayResponse = ClientGateway.GetGatewayResponse(appName);
            var popUpBody = Formatter.GetBody(gatewayResponse.Text, gatewayResponse.Attribution);

            if (!string.IsNullOrEmpty(popUpBody))
            {   
                DisplayPopUpMessage(popUpTitle, popUpBody, gatewayResponse.LinkUri);
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);
            }

            return hiddenOptionsDto;
        }

        private static HiddenOptionsDto GetHiddenOptionsDto(DateTime lastPopUpDateTime, int popUpCountToday)
        {
            var hiddenOptionsDto = new HiddenOptionsDto();

            var baseDateTime = DateTime.Now;

            //if last pop up was yesterday, then we have gone past midnight, so this is first pop up for today
            if (lastPopUpDateTime.Date < baseDateTime.Date)
            {
                hiddenOptionsDto.PopUpCountToday = 1;
            }
            else
            {
                hiddenOptionsDto.PopUpCountToday = popUpCountToday + 1;
            }

            hiddenOptionsDto.LastPopUpDateTime = baseDateTime;

            return hiddenOptionsDto;
        }

        private static void DisplayPopUpMessage(string popUpTitle, string popUpBody, string linkUri)
        {
            var triviaDialog = new TriviaDialog()
            {
                Title = popUpTitle,
                AppTextBlock1 = { Text = popUpBody }
            };

            if (!string.IsNullOrEmpty(linkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(linkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(linkUri);
            }

            triviaDialog.ShowModal();
        }
    }
}
