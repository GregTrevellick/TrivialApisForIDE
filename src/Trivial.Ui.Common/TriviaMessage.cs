using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.Shell.Interop;
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

            if (IsANewDay(lastPopUpDateTime, baseDateTime))
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

        private static bool IsANewDay(DateTime lastPopUpDateTime, DateTime baseDateTime)
        {
            //If last pop up was yesterday, then we have gone past midnight, so this is first pop up for today
            return lastPopUpDateTime.Date < baseDateTime.Date;
        }

        private static void DisplayPopUpMessage(string popUpTitle, string message, string linkUri, string date, string author, AppName appName)
        {
            const string spacer = " ";

            var triviaDialog = new TriviaDialog(appName)
            {
                AppTextBlockAttribution = { Text = author + spacer + date + spacer },
                AppTextBlockMessage = { Text = message },
                Title = popUpTitle,
            };

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockAttribution.Text))
            {
                triviaDialog.AppTextBlockAttribution.Visibility = Visibility.Visible;
                triviaDialog.AppTextBlock2.Visibility = Visibility.Visible;
            }

            var iconUri = GetIconUri(appName);
            triviaDialog.AppImage.Source = new BitmapImage(iconUri);

            if (!string.IsNullOrEmpty(linkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(linkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(linkUri);
            }

            triviaDialog.Show();
        }

        public static Uri GetIconUri(AppName appName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var imageSubDirectory = appName.ToString();
            var packUri = $"pack://application:,,,/{assemblyName.Name};component/Resources/{imageSubDirectory}/VsixExtensionIcon_16x16.png";
            return new Uri(packUri);
        }
    }
}
