using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using Trivial.Api.Gateway;
using Trivial.Api.Gateway.Jeopardy;
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

            var gatewayResponseJeopardy = (GatewayResponseJeopardy)gatewayResponse;
            var triviaDialogJeopardyDto = GetTriviaDialogJeopardyDto(appName, popUpTitle, optionsName, gatewayResponseJeopardy);
            DisplayPopUpMessageJeopardy(triviaDialogJeopardyDto);
            
            hiddenOptionsDto = GeekQuizGetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);

            return hiddenOptionsDto;
        }

        private static TriviaDialogJeopardyDto GetTriviaDialogJeopardyDto(AppName appName, string popUpTitle, string optionsName, GatewayResponseJeopardy gatewayResponseJeopardy)
        {
            var triviaDialogJeopardyDto =
                new TriviaDialogJeopardyDto(appName, optionsName, popUpTitle, gatewayResponseJeopardy.ErrorDetails)
                {
                    JeopardyAnswer = "A. " + gatewayResponseJeopardy.Answer,
                    JeopardyQuestion = "Q. " + gatewayResponseJeopardy.Question,
                };
            return triviaDialogJeopardyDto;
        }

        private void DisplayPopUpMessageJeopardy(TriviaDialogJeopardyDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName,
                triviaDialogDto.OptionsName)
            {
                AppTextBlockAnswerJeopardy = { Text = triviaDialogDto.JeopardyAnswer },
                AppBtnRevealAnswerJeopardy = { Content = triviaDialogDto.JeopardyAnswerRevealLabel },
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                Title = triviaDialogDto.PopUpTitle,
            };

            if (!string.IsNullOrWhiteSpace(triviaDialogDto.JeopardyQuestion))
            {
                var run = new Run(triviaDialogDto.JeopardyQuestion)
                {
                    FontWeight = FontWeights.Bold
                };
                triviaDialog.AppTextBlockQuestionJeopardy.Inlines.Add(run);
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockErrorDetails.Text))
            {
                triviaDialog.AppTextBlockErrorDetails.Visibility = Visibility.Visible;
            }

            if (triviaDialog.AppTextBlockQuestionJeopardy.Inlines.Any())
            {
                triviaDialog.AppTextBlockQuestionJeopardy.Visibility = Visibility.Visible;
            }

            if ((triviaDialog.AppBtnRevealAnswerJeopardy.Content != null) &&
                (!string.IsNullOrWhiteSpace(triviaDialog.AppBtnRevealAnswerJeopardy.Content.ToString())))
            {
                triviaDialog.AppBtnRevealAnswerJeopardy.Visibility = Visibility.Visible;
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
