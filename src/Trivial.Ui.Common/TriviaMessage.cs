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
        private string answer = string.Empty;
        private string quotationAuthor = string.Empty;
        private string quotationDate = string.Empty;
        private string hyperLinkUri = string.Empty;
        private string question = string.Empty;
        private string fact = string.Empty;
        private string quotation = string.Empty;
        private const string spacer = " ";
        private string attribution = string.Empty;

        public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds, CommonConstants.TimeOutInMilliSecondsOptionLabel, optionsName);

            var somethingToShow = false;

            switch (appName)
            {
                case AppName.Jeopardy:
                    var gatewayResponseJeopardy = (GatewayResponseJeopardy) gatewayResponse;
                    answer = "A. " + gatewayResponseJeopardy.Answer;
                    question = "Q. " + gatewayResponseJeopardy.Question;
                    somethingToShow = !string.IsNullOrEmpty(question);
                    break;
                case AppName.NumericTrivia:
                    var gatewayResponseNumeric = (GatewayResponseNumericTrivia)gatewayResponse;
                    fact = gatewayResponseNumeric.NumericFact;
                    somethingToShow = !string.IsNullOrEmpty(fact);
                    break;
                case AppName.TrumpQuotes:
                    var gatewayResponseTrump = (GatewayResponseTrumpQuotes)gatewayResponse;
                    quotationAuthor = gatewayResponseTrump.QuotationAuthor;
                    quotationDate = gatewayResponseTrump.QuotationDate;
                    hyperLinkUri = gatewayResponseTrump.HyperLinkUri;
                    quotation = gatewayResponseTrump.TrumpQuote;
                    attribution = quotationAuthor + spacer + quotationDate + spacer;
                    somethingToShow = !string.IsNullOrEmpty(quotation);
                    break;
            }         
          
            if (somethingToShow)
            {   
                DisplayPopUpMessage(popUpTitle, hyperLinkUri, attribution, appName, optionsName, answer, question, quotation, fact);
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);
            }

            return hiddenOptionsDto;
        }

        private void DisplayPopUpMessage(string popUpTitle, string linkUri, string attribution, AppName appName, string optionsName, string answer, string question, string quotation, string fact)
        {
            var triviaDialog = new TriviaDialog(appName, optionsName)
            {
                AppTextBlockAnswer = { Text = answer },
                AppTextBlockAttribution = { Text = attribution },
                AppTextBlockQuotation = { Text = quotation },
                AppTextBlockQuestion = { Text = question },
                AppTextBlockFact = { Text = fact },
                Title = popUpTitle,
            };

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

        public Uri GetIconUri(AppName appName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var imageSubDirectory = appName.ToString();
            var packUri = $"pack://application:,,,/{assemblyName.Name};component/Resources/{imageSubDirectory}/VsixExtensionIcon_16x16.png";
            return new Uri(packUri);
        }
    }
}
