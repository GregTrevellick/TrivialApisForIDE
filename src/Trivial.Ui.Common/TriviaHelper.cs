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

        private static bool MidweekAndHaveNotExceededMidweekCount(GeneralOptionsDto generalOptionsDto)//gregtlo unit test reqd
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

        public static HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds);

            var message = gatewayResponse.Text;
            var date = gatewayResponse.Attribution;
            var author = gatewayResponse.Attribution;

            if (!string.IsNullOrEmpty(message))
            {   
                DisplayPopUpMessage(popUpTitle, message, gatewayResponse.LinkUri, optionsName, date, author);
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

        private static void DisplayPopUpMessage(string popUpTitle, string message, string linkUri, string optionsName, string date, string author)
        {
            var triviaDialog = new TriviaDialog(optionsName)
            {
                AppTextBlockAuthor = { Text = author },
                AppTextBlockDate = { Text = date },
                AppTextBlockMessage = { Text = message },
                Title = popUpTitle,
            };

            if (!string.IsNullOrEmpty(linkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(linkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(linkUri);
            }

           // triviaDialog.ShowModal(); gregtlo offer user option to show as a modal
            triviaDialog.Show();

        }
    }
}
