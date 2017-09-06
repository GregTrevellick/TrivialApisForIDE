using System;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            if (//if weekend and haven't exceeded weekend count
                ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd)
                ||
                //if midweek and haven't exceeded midweek count
                ((DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday) &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay))
            {
                // if last popup more than X minutes ago
                if (generalOptionsDto.LastPopUpDateTime < DateTime.Now.AddMinutes(-1 * generalOptionsDto.PopUpIntervalInMins))
                {
                    return true;
                }
            }

            return false;
        }

        public static HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime)
        {
            HiddenOptionsDto hiddenOptionsDto = null; 

            var gatewayResponse = RestClient.GetGatewayResponse(appName);
            var popUpBody = Formatter.GetBody(gatewayResponse.Text, gatewayResponse.Attribution);

            if (!string.IsNullOrEmpty(popUpBody))
            {   
                DisplayPopUpMessage(popUpTitle, popUpBody, gatewayResponse.LinkUri);
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime);
            }

            return hiddenOptionsDto;
        }

        private static HiddenOptionsDto GetHiddenOptionsDto(DateTime lastPopUpDateTime)
        {
            var hiddenOptionsDto = new HiddenOptionsDto();

            var baseDateTime = DateTime.Now;

            //if last pop up was yesterday, then we have gone past midnight, so reset count for today to zero as it is a new day
            if (lastPopUpDateTime.Date < baseDateTime.Date)
            {
                hiddenOptionsDto.PopUpCountToday = 1;
            }
            else
            {
                hiddenOptionsDto.PopUpCountToday++;
            }

            hiddenOptionsDto.LastPopUpDateTime = baseDateTime;

            return hiddenOptionsDto;
        }

        //private static void GetGeneralOptionsDto()
        //{
        //    GeneralOptions.IsDirty = true;

        //    var baseDateTime = DateTime.Now;

        //    //if last pop up was yesterday, then we have gone past midnight, so reset count for today to zero as it is a new day
        //    if (GeneralOptions.LastPopUpDateTime.Date < baseDateTime.Date)
        //    {
        //        GeneralOptions.PopUpCountToday == 0;
        //    }

        //    GeneralOptions.PopUpCountToday++;
        //    GeneralOptions.LastPopUpDateTime = baseDateTime;
        //}

        private static void DisplayPopUpMessage(string popUpTitle, string popUpBody, string linkUri)
        {
            var triviaDialog = new TriviaDialog
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
