using System;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(DateTime nextPopUpDueDate)
        {
            if (nextPopUpDueDate >= DateTime.Now)
            {
                return true;
            }

            return false;
        }

        public static void ShowTrivia(AppName appName, string popUpTitle)
        {
            var gatewayResponse = RestClient.GetGatewayResponse(appName);
            var popUpBody = Formatter.GetBody(gatewayResponse.Text, gatewayResponse.Attribution);
            GetValue(popUpBody, gatewayResponse.Text, gatewayResponse.LinkUri, popUpTitle);
        }

        private static void GetValue(string popUpBody, string linkText, string linkUri, string popUpTitle)
        {
            if (!string.IsNullOrEmpty(popUpBody))
            {
                DisplayPopUpMessage(popUpTitle, popUpBody, linkText, linkUri);
            }
        }

        private static void DisplayPopUpMessage(string popUpTitle, string popUpBody, string linkText, string linkUri)
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
