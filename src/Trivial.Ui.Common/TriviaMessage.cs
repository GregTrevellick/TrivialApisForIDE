using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using Trivial.Api.Gateway;
using Trivial.Api.Gateway.Jeopardy;
using Trivial.Api.Gateway.NumericTrivia;
using Trivial.Api.Gateway.TrumpQuotes;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaMessage
    {
        private const string spacer = " ";

        public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds, CommonConstants.TimeOutInMilliSecondsOptionLabel, optionsName);

            var somethingToShow = false;
            var triviaDialogDto = new TriviaDialogDto
            {
                OptionsName = optionsName,
                PopUpTitle = popUpTitle
            };

            switch (appName)
            {
                case AppName.Jeopardy:
                    var gatewayResponseJeopardy = (GatewayResponseJeopardy) gatewayResponse;
                    triviaDialogDto.Answer = "A. " + gatewayResponseJeopardy.Answer;
                    triviaDialogDto.Question = "Q. " + gatewayResponseJeopardy.Question;
                    somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Question);
                    break;
                case AppName.NumericTrivia:
                    var gatewayResponseNumeric = (GatewayResponseNumericTrivia)gatewayResponse;
                    triviaDialogDto.Fact = gatewayResponseNumeric.NumericFact;
                    somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Fact);
                    break;
                case AppName.TrumpQuotes:
                    var gatewayResponseTrump = (GatewayResponseTrumpQuotes)gatewayResponse;
                    triviaDialogDto.HyperLinkUri = gatewayResponseTrump.HyperLinkUri;
                    triviaDialogDto.Quotation = gatewayResponseTrump.TrumpQuote;
                    triviaDialogDto.Attribution = gatewayResponseTrump.QuotationAuthor + spacer + gatewayResponseTrump.QuotationDate + spacer;
                    somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Quotation);
                    break;
            }         
          
            if (somethingToShow)
            {
                DisplayPopUpMessage(triviaDialogDto);
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);
            }

            return hiddenOptionsDto;
        }

        private void DisplayPopUpMessage(TriviaDialogDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(triviaDialogDto.AppName, triviaDialogDto.OptionsName)
            {
                AppTextBlockAnswer = {Text = triviaDialogDto.Answer},
                AppTextBlockAttribution = { Text =  triviaDialogDto.Attribution },
                AppTextBlockFact = { Text = triviaDialogDto.Fact },
                AppTextBlockQuestion = { Text = triviaDialogDto.Question},
                AppTextBlockQuotation = { Text =  triviaDialogDto.Quotation},
                Title = triviaDialogDto.PopUpTitle,
            };

            //gregt extract
            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockQuotation.Text))
            {
                triviaDialog.AppTextBlockQuotation.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockFact.Text))
            {
                triviaDialog.AppTextBlockFact.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockQuestion.Text))
            {
                triviaDialog.AppTextBlockQuestion.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockAttribution.Text))
            {
                triviaDialog.AppTextBlockAttribution.Visibility = Visibility.Visible;
                triviaDialog.AppTextBlockHyperLink.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockAnswer.Text))
            {
                triviaDialog.AppBtnRevealAnswer.Visibility = Visibility.Visible;
            }

            //gregt extract to icon method
            var iconUri = GetIconUri(triviaDialogDto.AppName);
            triviaDialog.AppImage.Source = new BitmapImage(iconUri);

            if (!string.IsNullOrEmpty(triviaDialogDto.HyperLinkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(triviaDialogDto.HyperLinkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(triviaDialogDto.HyperLinkUri);
            }

            triviaDialog.Show();
        }

        private HiddenOptionsDto GetHiddenOptionsDto(DateTime lastPopUpDateTime, int popUpCountToday)
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

        private bool IsANewDay(DateTime lastPopUpDateTime, DateTime baseDateTime)
        {
            //If last pop up was yesterday, then we have gone past midnight, so this is first pop up for today
            return lastPopUpDateTime.Date < baseDateTime.Date;
        }

        internal Uri GetIconUri(AppName appName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var imageSubDirectory = appName.ToString();
            var packUri = $"pack://application:,,,/{assemblyName.Name};component/Resources/{imageSubDirectory}/VsixExtensionIcon_16x16.png";
            return new Uri(packUri);
        }
    }
}
