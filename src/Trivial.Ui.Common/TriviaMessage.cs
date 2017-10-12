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
            var triviaDialogDto = new TriviaDialogDto
            {
                AppName = appName,
                OptionsName = optionsName,
                PopUpTitle = popUpTitle
            };

            if (!string.IsNullOrEmpty(gatewayResponse.ErrorDetails))
            {
                triviaDialogDto.ErrorDetails = gatewayResponse.ErrorDetails;
                somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.ErrorDetails);
            }
            else
            {
                //gregt extract 
                switch (appName)
                {
                    case AppName.GeekQuiz:
                        var gatewayResponseGeekQuiz = (GatewayResponseGeekQuiz)gatewayResponse;
                        triviaDialogDto.Difficulty = gatewayResponseGeekQuiz.DifficultyLevel;
                        triviaDialogDto.MultipleChoiceAnswers = gatewayResponseGeekQuiz.MultipleChoiceAnswers;
                        triviaDialogDto.MultipleChoiceCorrectAnswer = gatewayResponseGeekQuiz.MultipleChoiceCorrectAnswer;
                        triviaDialogDto.Question = gatewayResponseGeekQuiz.Question;
                        triviaDialogDto.QuestionType = gatewayResponseGeekQuiz.QuestionType;
                        DisplayPopUpMessageGeekQuiz(triviaDialogDto, suppressClosingWithoutSubmitingAnswerWarning, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Question);
                        break;
                    case AppName.Jeopardy:
                        var gatewayResponseJeopardy = (GatewayResponseJeopardy) gatewayResponse;
                        triviaDialogDto.Answer = "A. " + gatewayResponseJeopardy.Answer;
                        triviaDialogDto.Question = "Q. " + gatewayResponseJeopardy.Question;
                        DisplayPopUpMessageJeopardy(triviaDialogDto);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Question);
                        break;
                    case AppName.NumericTrivia:
                        var gatewayResponseNumeric = (GatewayResponseNumericTrivia) gatewayResponse;
                        triviaDialogDto.Fact = gatewayResponseNumeric.NumericFact;
                        DisplayPopUpMessageNumericTrivia(triviaDialogDto);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Fact);
                        break;
                    case AppName.TrumpQuotes:
                        var gatewayResponseTrump = (GatewayResponseTrumpQuotes) gatewayResponse;
                        triviaDialogDto.HyperLinkUri = gatewayResponseTrump.HyperLinkUri;
                        triviaDialogDto.Quotation = gatewayResponseTrump.TrumpQuote;
                        triviaDialogDto.Attribution = gatewayResponseTrump.QuotationAuthor + spacer + gatewayResponseTrump.QuotationDate + spacer;
                        DisplayPopUpMessageTrumpQuotes(triviaDialogDto);
                        somethingToShow = !string.IsNullOrEmpty(triviaDialogDto.Quotation);
                        break;
                }
            }

            if (somethingToShow)
            {
                //DisplayPopUpMessage(triviaDialogDto, suppressClosingWithoutSubmitingAnswerWarning, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);
            }

            return hiddenOptionsDto;
        }

        void PersistHiddenOptions(int? totalQuestionsAsked, int? totalQuestionsAnsweredCorrectly)
        {
            PersistHiddenOptionsEventHandler2?.Invoke(totalQuestionsAsked, totalQuestionsAnsweredCorrectly);
        }

        private void DisplayPopUpMessageGeekQuiz(TriviaDialogDto triviaDialogDto, bool? suppressClosingWithoutSubmitingAnswerWarning, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName, 
                triviaDialogDto.OptionsName,
                suppressClosingWithoutSubmitingAnswerWarning,
                triviaDialogDto.MultipleChoiceCorrectAnswer, 
                triviaDialogDto.QuestionType,
                totalQuestionsAnsweredCorrectly, 
                totalQuestionsAsked)
            {
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                Title = triviaDialogDto.PopUpTitle,
            };

            triviaDialog.PersistHiddenOptionsEventHandler += PersistHiddenOptions;

            if (!string.IsNullOrWhiteSpace(triviaDialogDto.Difficulty))
            {
                var run = new Run(triviaDialogDto.Difficulty);
                triviaDialog.AppTextBlockQuestionJeopardy.Inlines.Add(run);
            }

            if (!string.IsNullOrWhiteSpace(triviaDialogDto.Question))
            {
                var run = new Run(triviaDialogDto.Question)
                {
                    FontWeight = FontWeights.Bold
                };
                triviaDialog.AppTextBlockQuestionGeekQuiz.Inlines.Add(run);
            }

            if (!string.IsNullOrWhiteSpace(triviaDialog.AppTextBlockErrorDetails.Text))
            {
                triviaDialog.AppTextBlockErrorDetails.Visibility = Visibility.Visible;
            }

            if (triviaDialogDto.QuestionType != QuestionType.None)
            {
                triviaDialog.AppBtnGeekQuizSubmitMultiChoiceAnwser.Visibility= Visibility.Visible;

                if (triviaDialogDto.QuestionType == QuestionType.TrueFalse)
                {
                    var trueFollowedByFalseAnswers = triviaDialogDto.MultipleChoiceAnswers.OrderByDescending(x => x).Select(x => x).ToArray();
                    triviaDialog.RadioBtn1.Content = trueFollowedByFalseAnswers[0];
                    triviaDialog.RadioBtn2.Content = trueFollowedByFalseAnswers[1];
                }
                else
                {
                    var random = new Random();
                    var randomlySortedAnswers = triviaDialogDto.MultipleChoiceAnswers.OrderBy(x => random.Next()).Select(x => x).ToArray();
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

        private void DisplayPopUpMessageJeopardy(TriviaDialogDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName,
                triviaDialogDto.OptionsName)
            {
                AppTextBlockAnswerJeopardy = { Text = triviaDialogDto.Answer },
                AppBtnRevealAnswerJeopardy = { Content = triviaDialogDto.AnswerRevealLabel },
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                Title = triviaDialogDto.PopUpTitle,
            };

            if (!string.IsNullOrWhiteSpace(triviaDialogDto.Question))
            {
                var run = new Run(triviaDialogDto.Question)
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

        private void DisplayPopUpMessageNumericTrivia(TriviaDialogDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName,
                triviaDialogDto.OptionsName)
            {
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                AppTextBlockFactNumericTrivia = { Text = triviaDialogDto.Fact },
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

        private void DisplayPopUpMessageTrumpQuotes(TriviaDialogDto triviaDialogDto)
        {
            var triviaDialog = new TriviaDialog(
                triviaDialogDto.AppName,
                triviaDialogDto.OptionsName)
            {
                AppTextBlockAttributionTrumpQuotes = { Text = triviaDialogDto.Attribution },
                AppTextBlockErrorDetails = { Text = triviaDialogDto.ErrorDetails },
                AppTextBlockQuotationTrumpQuotes = { Text = triviaDialogDto.Quotation },
                Title = triviaDialogDto.PopUpTitle,
            };

            if (!string.IsNullOrEmpty(triviaDialogDto.HyperLinkUri))
            {
                triviaDialog.AppHyperlink1TrumpQuotes.NavigateUri = new Uri(triviaDialogDto.HyperLinkUri);
                triviaDialog.AppHyperlink1TrumpQuotes.Inlines.Clear();
                triviaDialog.AppHyperlink1TrumpQuotes.Inlines.Add(triviaDialogDto.HyperLinkUri);
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
