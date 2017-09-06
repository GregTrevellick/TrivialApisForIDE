using System;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            if (
                (generalOptionsDto.PopUpIntervalInMins == 0 && generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay) 
                ||
                (generalOptionsDto.LastPopUpDateTime >= DateTime.Now && generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay)
               )
            {
                return true;
            }

            return false;
        }

        public static void ShowTrivia(AppName appName, string popUpTitle)
        {
            var gatewayResponse = RestClient.GetGatewayResponse(appName);
            var popUpBody = Formatter.GetBody(gatewayResponse.Text, gatewayResponse.Attribution);

            if (!string.IsNullOrEmpty(popUpBody))
            {   
                DisplayPopUpMessage(popUpTitle, popUpBody, gatewayResponse.LinkUri);
 //               Update_GeneralOptions_Dot_LastPopUpDateTime(DateTime.Now);
            }
        }

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
