using System;
using System.Reflection;
using System.Windows;
using Trivial.Api.Gateway;
using Trivial.Api.Gateway.NumericTrivia;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaMessage
    {
        public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName)
        {
            return ShowTriviaMessage(appName, popUpTitle, lastPopUpDateTime, popUpCountToday, timeOutInMilliSeconds, optionsName, null, null, null);
        }

        //public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName, bool suppressClosingWithoutSubmitingAnswerWarning, int totalQuestionsAnsweredCorrectly, int totalQuestionsAsked)
        //{
        //    return ShowTriviaMessage(appName, popUpTitle, lastPopUpDateTime, popUpCountToday, timeOutInMilliSeconds, optionsName, suppressClosingWithoutSubmitingAnswerWarning, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
        //}

        private HiddenOptionsDto ShowTriviaMessage(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName, bool? suppressClosingWithoutSubmitingAnswerWarning, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds, CommonConstants.TimeOutInMilliSecondsOptionLabel, optionsName);

            var gatewayResponseNumeric = (GatewayResponseNumericTrivia)gatewayResponse;
            var triviaDialogNumericTriviaDto = GetTriviaDialogNumericTriviaDto(appName, popUpTitle, optionsName, gatewayResponseNumeric);
            DisplayPopUpMessageNumericTrivia(triviaDialogNumericTriviaDto);

            hiddenOptionsDto = GeekQuizGetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);

            return hiddenOptionsDto;
        }

        private static TriviaDialogNumericTriviaDto GetTriviaDialogNumericTriviaDto(AppName appName, string popUpTitle, string optionsName, GatewayResponseNumericTrivia gatewayResponseNumeric)
        {
            var triviaDialogNumericTriviaDto =
                new TriviaDialogNumericTriviaDto(appName, optionsName, popUpTitle, gatewayResponseNumeric.ErrorDetails)
                {
                    NumericTriviaFact = gatewayResponseNumeric.NumericFact,
                };
            return triviaDialogNumericTriviaDto;
        }

        private void DisplayPopUpMessageNumericTrivia(TriviaDialogNumericTriviaDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName,
                triviaDialogDto.OptionsName)
            {
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                AppTextBlockFactNumericTrivia = { Text = triviaDialogDto.NumericTriviaFact },
                Title = triviaDialogDto.PopUpTitle,
            };

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockErrorDetails.Text))
            {
                triviaDialog.AppTextBlockErrorDetails.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockFactNumericTrivia.Text))
            {
                triviaDialog.AppTextBlockFactNumericTrivia.Visibility = Visibility.Visible;
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
