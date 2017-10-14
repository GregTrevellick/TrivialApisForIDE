using System;
using System.Reflection;
using System.Windows;
using Trivial.Api.Gateway;
using Trivial.Api.Gateway.TrumpQuotes;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaMessage
    {
        private const string spacer = " ";
      //  public delegate void MyEventHandler2(int? totalQuestionsAsked, int? totalQuestionsAnsweredCorrectly);
      //  public event MyEventHandler2 PersistHiddenOptionsEventHandler2;//gregt rename to remove '2' suffix

        public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName)
        {
            return ShowTriviaMessage(appName, popUpTitle, lastPopUpDateTime, popUpCountToday, timeOutInMilliSeconds, optionsName, null, null, null);
        }

        //public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName, bool suppressClosingWithoutSubmitingAnswerWarning, int totalQuestionsAnsweredCorrectly, int totalQuestionsAsked)
       // {
         //   return ShowTriviaMessage(appName, popUpTitle, lastPopUpDateTime, popUpCountToday, timeOutInMilliSeconds, optionsName, suppressClosingWithoutSubmitingAnswerWarning, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
        //}

        private HiddenOptionsDto ShowTriviaMessage(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName, bool? suppressClosingWithoutSubmitingAnswerWarning, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds, CommonConstants.TimeOutInMilliSecondsOptionLabel, optionsName);

            var gatewayResponseTrump = (GatewayResponseTrumpQuotes)gatewayResponse;
            var triviaDialogTrumpQuotesDto = GetTriviaDialogTrumpQuotesDto(appName, popUpTitle, optionsName, gatewayResponseTrump);
            DisplayPopUpMessageTrumpQuotes(triviaDialogTrumpQuotesDto);

            hiddenOptionsDto = GeekQuizGetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);

            return hiddenOptionsDto;
        }

        private static TriviaDialogTrumpQuotesDto GetTriviaDialogTrumpQuotesDto(AppName appName, string popUpTitle, string optionsName, GatewayResponseTrumpQuotes gatewayResponseTrump)
        {
            var triviaDialogTrumpQuotesDto =
                new TriviaDialogTrumpQuotesDto(appName, optionsName, popUpTitle, gatewayResponseTrump.ErrorDetails)
                {
                    TrumpQuotesAttribution = gatewayResponseTrump.QuotationAuthor + spacer +
                                             gatewayResponseTrump.QuotationDate + spacer,
                    TrumpQuotesHyperLinkUri = gatewayResponseTrump.HyperLinkUri,
                    TrumpQuotesQuotation = gatewayResponseTrump.TrumpQuote,
                };
            return triviaDialogTrumpQuotesDto;
        }

        private void DisplayPopUpMessageTrumpQuotes(TriviaDialogTrumpQuotesDto triviaDialogDto)
        {
            var triviaDialog = new TrumpQuotes.TriviaDialog(
                triviaDialogDto.AppName,
                triviaDialogDto.OptionsName)
            {
                AppTextBlockAttributionTrumpQuotes = { Text = triviaDialogDto.TrumpQuotesAttribution },
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                AppTextBlockQuotationTrumpQuotes = { Text = triviaDialogDto.TrumpQuotesQuotation },
                Title = triviaDialogDto.PopUpTitle,
            };

            if (!string.IsNullOrEmpty(triviaDialogDto.TrumpQuotesHyperLinkUri))
            {
                triviaDialog.AppHyperlink1TrumpQuotes.NavigateUri = new Uri(triviaDialogDto.TrumpQuotesHyperLinkUri);
                triviaDialog.AppHyperlink1TrumpQuotes.Inlines.Clear();
                triviaDialog.AppHyperlink1TrumpQuotes.Inlines.Add(triviaDialogDto.TrumpQuotesHyperLinkUri);
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockErrorDetails.Text))
            {
                triviaDialog.AppTextBlockErrorDetails.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockQuotationTrumpQuotes.Text))
            {
                triviaDialog.AppTextBlockQuotationTrumpQuotes.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockAttributionTrumpQuotes.Text))
            {
                triviaDialog.AppTextBlockAttributionTrumpQuotes.Visibility = Visibility.Visible;
                triviaDialog.AppTextBlockHyperLinkTrumpQuotes.Visibility = Visibility.Visible;
            }

            triviaDialog.Show();
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
