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
                optionsName = optionsName,
                popUpTitle = popUpTitle
            };

            switch (appName)
            {
                case AppName.Jeopardy:
                    var gatewayResponseJeopardy = (GatewayResponseJeopardy) gatewayResponse;
                    triviaDialogDto.answer = "A. " + gatewayResponseJeopardy.Answer;
                    triviaDialogDto.question = "Q. " + gatewayResponseJeopardy.Question;
                    somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.question);
                    break;
                case AppName.NumericTrivia:
                    var gatewayResponseNumeric = (GatewayResponseNumericTrivia)gatewayResponse;
                    triviaDialogDto.fact = gatewayResponseNumeric.NumericFact;
                    somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.fact);
                    break;
                case AppName.TrumpQuotes:
                    var gatewayResponseTrump = (GatewayResponseTrumpQuotes)gatewayResponse;
                    triviaDialogDto.hyperLinkUri = gatewayResponseTrump.HyperLinkUri;
                    triviaDialogDto.quotation = gatewayResponseTrump.TrumpQuote;
                    triviaDialogDto.attribution = gatewayResponseTrump.QuotationAuthor + spacer + gatewayResponseTrump.QuotationDate + spacer;
                    somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.quotation);
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
            var triviaDialog = new TriviaDialog(triviaDialogDto.appName, triviaDialogDto.optionsName)
            {
                AppTextBlockAnswer = {Text = triviaDialogDto.answer},// == null ? string.Empty : triviaDialogDto.answer},
                AppTextBlockAttribution = { Text =  triviaDialogDto.attribution },//== null ? string.Empty : triviaDialogDto.attribution },
                AppTextBlockFact = { Text = triviaDialogDto.fact },//== null ? string.Empty : triviaDialogDto.fact },
                AppTextBlockQuestion = { Text = triviaDialogDto.question},// == null ? string.Empty : triviaDialogDto.question },
                AppTextBlockQuotation = { Text =  triviaDialogDto.quotation},// == null ? string.Empty : triviaDialogDto.quotation },
                Title = triviaDialogDto.popUpTitle,
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
            var iconUri = GetIconUri(triviaDialogDto.appName);
            triviaDialog.AppImage.Source = new BitmapImage(iconUri);

            if (!string.IsNullOrEmpty(triviaDialogDto.hyperLinkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(triviaDialogDto.hyperLinkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(triviaDialogDto.hyperLinkUri);
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
