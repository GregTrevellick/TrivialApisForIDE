using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Trivial.Api.Gateway;
using Trivial.Api.Gateway.GeekQuiz;
using Trivial.Api.Gateway.Jeopardy;
using Trivial.Api.Gateway.NumericTrivia;
using Trivial.Api.Gateway.TrumpQuotes;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaMessage
    {
        private const string spacer = " ";
        public delegate void MyEventHandler2(int? totalQuestionsAsked, int? totalQuestionsAnsweredCorrectly);
        public event MyEventHandler2 PersistHiddenOptionsEventHandler2;//gregt rename to remove '2' suffix

        public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName)
        {
            return ShowTriviaMessage(appName, popUpTitle, lastPopUpDateTime, popUpCountToday, timeOutInMilliSeconds, optionsName, null, null, null);
        }

        public HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName, bool suppressClosingWithoutSubmitingAnswerWarning, int totalQuestionsAnsweredCorrectly, int totalQuestionsAsked)
        {
            return ShowTriviaMessage(appName, popUpTitle, lastPopUpDateTime, popUpCountToday, timeOutInMilliSeconds, optionsName, suppressClosingWithoutSubmitingAnswerWarning, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
        }

        private HiddenOptionsDto ShowTriviaMessage(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds, string optionsName, bool? suppressClosingWithoutSubmitingAnswerWarning, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds, CommonConstants.TimeOutInMilliSecondsOptionLabel, optionsName);

            var somethingToShow = false;
            //var triviaDialogDtoBase = new TriviaDialogDtoBase
            //{
            //    AppName = appName,
            //    OptionsName = optionsName,
            //    PopUpTitle = popUpTitle
            //};

            if (!string.IsNullOrEmpty(gatewayResponse.ErrorDetails))
            {
                //TODO gregt gregt gregt triviaDialogDtoBase.ErrorDetails = gatewayResponse.ErrorDetails;
                //TODO gregt gregt gregt somethingToShow = !string.IsNullOrEmpty(triviaDialogDtoBase.ErrorDetails);
            }
            else
            {
                //gregt extract 
                switch (appName)
                {
                    case AppName.GeekQuiz:
                        var gatewayResponseGeekQuiz = (GatewayResponseGeekQuiz)gatewayResponse;
                        var triviaDialogGeekQuizDto = new TriviaDialogGeekQuizDto();
                        triviaDialogGeekQuizDto.AppName = appName;
                        triviaDialogGeekQuizDto.OptionsName = optionsName;
                        triviaDialogGeekQuizDto.PopUpTitle = popUpTitle;
                        triviaDialogGeekQuizDto.GeekQuizDifficulty = gatewayResponseGeekQuiz.DifficultyLevel;
                        triviaDialogGeekQuizDto.GeekQuizMultipleChoiceAnswers = gatewayResponseGeekQuiz.MultipleChoiceAnswers;
                        triviaDialogGeekQuizDto.GeekQuizMultipleChoiceCorrectAnswer = gatewayResponseGeekQuiz.MultipleChoiceCorrectAnswer;
                        triviaDialogGeekQuizDto.GeekQuizQuestion = gatewayResponseGeekQuiz.Question;
                        triviaDialogGeekQuizDto.GeekQuizQuestionType = gatewayResponseGeekQuiz.QuestionType;
                        DisplayPopUpMessageGeekQuiz(triviaDialogGeekQuizDto, suppressClosingWithoutSubmitingAnswerWarning, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogGeekQuizDto.GeekQuizQuestion);
                        break;
                    case AppName.Jeopardy:
                        var gatewayResponseJeopardy = (GatewayResponseJeopardy) gatewayResponse;
                        var triviaDialogJeopardyDto = new TriviaDialogJeopardyDto();
                        triviaDialogJeopardyDto.AppName = appName;
                        triviaDialogJeopardyDto.OptionsName = optionsName;
                        triviaDialogJeopardyDto.PopUpTitle = popUpTitle;
                        triviaDialogJeopardyDto.JeopardyAnswer = "A. " + gatewayResponseJeopardy.Answer;
                        triviaDialogJeopardyDto.JeopardyQuestion = "Q. " + gatewayResponseJeopardy.Question;
                        DisplayPopUpMessageJeopardy(triviaDialogJeopardyDto);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogJeopardyDto.JeopardyQuestion);
                        break;
                    case AppName.NumericTrivia:
                        var gatewayResponseNumeric = (GatewayResponseNumericTrivia) gatewayResponse;
                        var triviaDialogNumericTriviaDto = new TriviaDialogNumericTriviaDto();
                        triviaDialogNumericTriviaDto.AppName = appName;
                        triviaDialogNumericTriviaDto.OptionsName = optionsName;
                        triviaDialogNumericTriviaDto.PopUpTitle = popUpTitle;
                        triviaDialogNumericTriviaDto.NumericTriviaFact = gatewayResponseNumeric.NumericFact;
                        DisplayPopUpMessageNumericTrivia(triviaDialogNumericTriviaDto);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogNumericTriviaDto.NumericTriviaFact);
                        break;
                    case AppName.TrumpQuotes:
                        var gatewayResponseTrump = (GatewayResponseTrumpQuotes) gatewayResponse;
                        var triviaDialogTrumpQuotesDto = new TriviaDialogTrumpQuotesDto();
                        triviaDialogTrumpQuotesDto.AppName = appName;
                        triviaDialogTrumpQuotesDto.OptionsName = optionsName;
                        triviaDialogTrumpQuotesDto.PopUpTitle = popUpTitle;
                        triviaDialogTrumpQuotesDto.TrumpQuotesHyperLinkUri = gatewayResponseTrump.HyperLinkUri;
                        triviaDialogTrumpQuotesDto.TrumpQuotesQuotation = gatewayResponseTrump.TrumpQuote;
                        triviaDialogTrumpQuotesDto.TrumpQuotesAttribution = gatewayResponseTrump.QuotationAuthor + spacer + gatewayResponseTrump.QuotationDate + spacer;
                        DisplayPopUpMessageTrumpQuotes(triviaDialogTrumpQuotesDto);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogTrumpQuotesDto.TrumpQuotesQuotation);
                        break;
                }
            }

            if (somethingToShow)
            {
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);
            }

            return hiddenOptionsDto;
        }

        void PersistHiddenOptions(int? totalQuestionsAsked, int? totalQuestionsAnsweredCorrectly)
        {
            PersistHiddenOptionsEventHandler2?.Invoke(totalQuestionsAsked, totalQuestionsAnsweredCorrectly);
        }

        private void DisplayPopUpMessageGeekQuiz(TriviaDialogGeekQuizDto triviaDialogDto, bool? suppressClosingWithoutSubmitingAnswerWarning, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName, 
                triviaDialogDto.OptionsName,
                suppressClosingWithoutSubmitingAnswerWarning,
                triviaDialogDto.GeekQuizMultipleChoiceCorrectAnswer, 
                triviaDialogDto.GeekQuizQuestionType,
                totalQuestionsAnsweredCorrectly, 
                totalQuestionsAsked)
            {
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                Title = triviaDialogDto.PopUpTitle,
            };

            triviaDialog.PersistHiddenOptionsEventHandler += PersistHiddenOptions;

            if (!string.IsNullOrWhiteSpace(triviaDialogDto.GeekQuizDifficulty))
            {
                var run = new Run(triviaDialogDto.GeekQuizDifficulty);
                triviaDialog.AppTextBlockQuestionJeopardy.Inlines.Add(run);
            }

            if (!string.IsNullOrWhiteSpace(triviaDialogDto.GeekQuizQuestion))
            {
                var run = new Run(triviaDialogDto.GeekQuizQuestion)
                {
                    FontWeight = FontWeights.Bold
                };
                triviaDialog.AppTextBlockQuestionGeekQuiz.Inlines.Add(run);
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockErrorDetails.Text))
            {
                triviaDialog.AppTextBlockErrorDetails.Visibility = Visibility.Visible;
            }

            if (triviaDialogDto.GeekQuizQuestionType != QuestionType.None)
            {
                triviaDialog.AppBtnGeekQuizSubmitMultiChoiceAnwser.Visibility= Visibility.Visible;

                if (triviaDialogDto.GeekQuizQuestionType == QuestionType.TrueFalse)
                {
                    var trueFollowedByFalseAnswers = triviaDialogDto.GeekQuizMultipleChoiceAnswers.OrderByDescending(x => x).Select(x => x).ToArray();
                    triviaDialog.RadioBtn1.Content = trueFollowedByFalseAnswers[0];
                    triviaDialog.RadioBtn2.Content = trueFollowedByFalseAnswers[1];
                }
                else
                {
                    var random = new Random();
                    var randomlySortedAnswers = triviaDialogDto.GeekQuizMultipleChoiceAnswers.OrderBy(x => random.Next()).Select(x => x).ToArray();
                    triviaDialog.RadioBtn1.Content = randomlySortedAnswers[0];
                    triviaDialog.RadioBtn2.Content = randomlySortedAnswers[1];
                    triviaDialog.RadioBtn3.Content = randomlySortedAnswers[2];
                    triviaDialog.RadioBtn4.Content = randomlySortedAnswers[3];
                }
            }

            SetRadioButtonVisibility(triviaDialog.RadioBtn1);
            SetRadioButtonVisibility(triviaDialog.RadioBtn2);
            SetRadioButtonVisibility(triviaDialog.RadioBtn3);
            SetRadioButtonVisibility(triviaDialog.RadioBtn4);

            if (totalQuestionsAnsweredCorrectly.HasValue && totalQuestionsAsked.HasValue)
            {
                var userStatus = triviaDialog.GetUserStatus(totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
                triviaDialog.AppTextBlockGeekQuizUserStatus.Text = userStatus;
                triviaDialog.AppTextBlockGeekQuizUserStatus.Visibility = Visibility.Visible;
            }

            triviaDialog.Show();
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
                triviaDialog.AppTextBlockQuestionGeekQuiz.Inlines.Add(run);
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

        private void DisplayPopUpMessageTrumpQuotes(TriviaDialogTrumpQuotesDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(
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

        private void SetRadioButtonVisibility(RadioButton radioButton)
        {
            if (radioButton.Content != null && !string.IsNullOrWhiteSpace(radioButton.Content.ToString()))
            {
                radioButton.Visibility = Visibility.Visible;
            }
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
