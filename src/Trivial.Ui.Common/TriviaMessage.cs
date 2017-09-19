using System;
using System.Windows.Media.Imaging;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public static class TriviaMessage
    {
        public static HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds);

            var author = gatewayResponse.Author;
            var date = gatewayResponse.Date;
            var message = gatewayResponse.Text;

            if (!string.IsNullOrEmpty(message))
            {   
                DisplayPopUpMessage(popUpTitle, message, gatewayResponse.LinkUri, date, author, appName);
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

        private static void DisplayPopUpMessage(string popUpTitle, string message, string linkUri, string date, string author, AppName appName)
        {
            const string spacer = " ";

            var triviaDialog = new TriviaDialog
            {
                AppTextBlockAttribution = { Text = author + spacer + date + spacer },
                AppTextBlockMessage = { Text = message },
                Title = popUpTitle,
            };

            var uri = GetUri(appName);
            triviaDialog.AppImage.Source = new BitmapImage(uri);

            if (!string.IsNullOrEmpty(linkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(linkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(linkUri);
            }

            triviaDialog.Show();
        }

        private static Uri GetUri(AppName appName)
        {
            var assemblyName = "Trivial.Ui.Common"; //gregt get assembly name
            var imageSubDirectory = appName.ToString();
            var packUri = "pack://application:,,,/"
                          + assemblyName 
                          + ";component/Resources/"
                          + imageSubDirectory
                          + "/VsixExtensionIcon_16x16.png";

            return new Uri(packUri);
        }
    }
}
