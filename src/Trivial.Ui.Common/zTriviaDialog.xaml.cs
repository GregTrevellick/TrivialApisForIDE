using System;
using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public partial class zTriviaDialog : DialogWindow
    {
        private AppName _appName;
        private string _correctAnswer;
        private string _optionsName;
        public QuestionType _questionType;
        public bool _suppressClosingWithoutSubmitingAnswerWarning;
        public int? _totalQuestionsAnsweredCorrectly;
        public int? _totalQuestionsAsked;
        private bool _userStatusTotalsIncremented;
        public delegate void MyEventHandler(int? totalQuestionsAsked, int? totalQuestionsAnsweredCorrectly);
        public event MyEventHandler PersistHiddenOptionsEventHandler;

        public zTriviaDialog(AppName appName, string optionsName)
        {
            _appName = appName;
            _optionsName = optionsName;
            _userStatusTotalsIncremented = false;

            InitializeComponent();
            InitializeTriviaDialog();

            DataContext = this;

            switch (_appName)
            {
                case AppName.GeekQuiz:
                    StackPanelGeekQuiz.Visibility = Visibility.Visible;
                    break;
                case AppName.Jeopardy:
                    StackPanelJeopardy.Visibility = Visibility.Visible;
                    break;
                case AppName.NumericTrivia:
                    StackPanelNumericTrivia.Visibility = Visibility.Visible;
                    break;
                case AppName.TrumpQuotes:
                    StackPanelTrumpQuotes.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void InitializeTriviaDialog()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //var iconUri = new TriviaMessage().GetIconUri(_appName);
            //Icon = new BitmapImage(iconUri);
        }

        private void AppBtnHelp_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBlockHelp.Visibility == Visibility.Visible)
            {
                TextBlockHelp.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockHelp.Text = $"To alter frequency and volume of delivery go to Tools | Options | {_optionsName}";
                TextBlockHelp.Visibility = Visibility.Visible;
            }
        }

        private void AppBtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            var shouldClose = true;

            if (_questionType != QuestionType.None)
            {
                if (AppBtnGeekQuizSubmitMultiChoiceAnwser.IsEnabled)
                {
                    if (!_suppressClosingWithoutSubmitingAnswerWarning)
                    {
                        var closeWithoutSubmitingAnswer = MessageBoxes.ConfirmCloseWithoutSubmitingAnswer(_optionsName);

                        if (!closeWithoutSubmitingAnswer)
                        {
                            shouldClose = false;
                        }
                    }
                }
            }

            if (shouldClose)
            {
                Close();
            }
        }

        #region not shared
        private void AppHyperlink1_RequestNavigate(object sender, RequestNavigateEventArgs e)//TrumpQuotes
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void AppBtnRevealAnswer_OnClick(object sender, RoutedEventArgs e)//Jeopardy
        {
            AppBtnRevealAnswerJeopardy.Visibility = Visibility.Collapsed;
            AppTextBlockAnswerJeopardy.Visibility = Visibility.Visible;
        }

        private void AppBtnSubmitMultiChoiceAnwser_OnClick(object sender, RoutedEventArgs e)//GeekQuiz
        {
            string response;

            if (RadioBtn1.IsChecked == true)
            {
                response = RadioBtn1.Content.ToString();
            }
            else
            {
                if (RadioBtn2.IsChecked == true)
                {
                    response = RadioBtn2.Content.ToString();
                }
                else
                {
                    if (RadioBtn3.IsChecked == true)
                    {
                        response = RadioBtn3.Content.ToString();
                    }
                    else
                    {
                        if (RadioBtn4.IsChecked == true)
                        {
                            response = RadioBtn4.Content.ToString();
                        }
                        else
                        {
                            response = null;
                        }
                    }
                }
            }

            ActOnAnswerGivenGeekQuiz(response);
        }

        private void ActOnAnswerGivenGeekQuiz(string response)
        {
            GeekQuizReplyEmoticonCorrect.Visibility = Visibility.Collapsed;
            GeekQuizReplyEmoticonIncorrect.Visibility = Visibility.Collapsed;

            var isResponseCorrect = IsResponseCorrectGeekQuiz(response);

            if (isResponseCorrect)
            {
                TextBlockGeekQuizReply.Text = "Well,done - correct answer !";
                SetQuizReplyColourGeekQuiz(Colors.Green);
                AppBtnGeekQuizSubmitMultiChoiceAnwser.IsEnabled = false;
                GeekQuizReplyEmoticonCorrect.Visibility = Visibility.Visible;

                if (!_userStatusTotalsIncremented && _totalQuestionsAnsweredCorrectly.HasValue)
                {
                    _totalQuestionsAnsweredCorrectly++;
                }
            }
            else
            {
                if (response == null)
                {
                    TextBlockGeekQuizReply.Text = "No cheating please - you must supply an answer.";
                    SetQuizReplyColourGeekQuiz(Colors.Orange);
                }
                else
                {
                    TextBlockGeekQuizReply.Text = "Oh dear - wrong answer.";

                    if (_questionType == QuestionType.MultiChoice)
                    {
                        TextBlockGeekQuizReply.Text += $" The correct answer is {_correctAnswer}";
                    }

                    AppBtnGeekQuizSubmitMultiChoiceAnwser.IsEnabled = false;
                    SetQuizReplyColourGeekQuiz(Colors.Red);
                    GeekQuizReplyEmoticonIncorrect.Visibility = Visibility.Visible;
                }
            }

            TextBlockGeekQuizReply.Visibility = Visibility.Visible;

            if (!_userStatusTotalsIncremented && _totalQuestionsAsked.HasValue)
            {
                _totalQuestionsAsked++;
                _userStatusTotalsIncremented = true;
            }
            var userStatus = GetUserStatusGeekQuiz(_totalQuestionsAnsweredCorrectly, _totalQuestionsAsked);
            AppTextBlockGeekQuizUserStatus.Text = userStatus;

            PersistHiddenOptionsEventHandler?.Invoke(_totalQuestionsAsked, _totalQuestionsAnsweredCorrectly);
        }

        internal string GetUserStatusGeekQuiz(int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            int percentageSuccess;

            if (totalQuestionsAsked == 0 ||
                !totalQuestionsAnsweredCorrectly.HasValue ||
                !totalQuestionsAsked.HasValue)
            {
                percentageSuccess = 0;
            }
            else
            {
                //gregt unit test required
                double percentage = ((double)totalQuestionsAnsweredCorrectly.Value / totalQuestionsAsked.Value) * 100;
                percentageSuccess = (int)Math.Round(percentage, MidpointRounding.AwayFromZero);
            }

            var userStatusDescription = percentageSuccess.UserStatusDescription();

            var ranking = "Your rank: " + userStatusDescription;

            var successRate = percentageSuccess + "% success rate (" +
                              totalQuestionsAnsweredCorrectly + " out of " +
                              totalQuestionsAsked + ")";

            var userStatus = ranking + " " + successRate;

            return userStatus;
        }

        private void SetQuizReplyColourGeekQuiz(Color color)
        {
            TextBlockGeekQuizReply.Foreground = new SolidColorBrush(color);
        }

        private bool IsResponseCorrectGeekQuiz(string response)
        {
            var rightAnswer = response == _correctAnswer;

            return rightAnswer;
        }
        #endregion
    }
}