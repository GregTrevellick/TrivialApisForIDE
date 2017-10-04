﻿using System;
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
    public partial class TriviaDialog : DialogWindow
    {
        private AppName _appName;
        private string _correctAnswer;
        private string _optionsName;
        private QuestionType _questionType;
        private bool _submitAnswerButtonClicked;
        private bool? _suppressClosingWithoutSubmitingAnswerWarning;
        private int? _totalQuestionsAnsweredCorrectly;
        private int? _totalQuestionsAsked;

        public TriviaDialog(AppName appName, string optionsName, bool? suppressClosingWithoutSubmitingAnswerWarning)
        {
            Init(appName, optionsName, suppressClosingWithoutSubmitingAnswerWarning, null, QuestionType.None, null, null);
        }

        public TriviaDialog(AppName appName, string optionsName, bool? suppressClosingWithoutSubmitingAnswerWarning, string correctAnswer, QuestionType questionType, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            Init(appName, optionsName, suppressClosingWithoutSubmitingAnswerWarning, correctAnswer, questionType, totalQuestionsAnsweredCorrectly, totalQuestionsAsked);
        }

        private void Init(AppName appName, string optionsName, bool? suppressClosingWithoutSubmitingAnswerWarning, string correctAnswer, QuestionType questionType, int? totalQuestionsAnsweredCorrectly, int? totalQuestionsAsked)
        {
            _appName = appName;
            _correctAnswer = correctAnswer;
            _optionsName = optionsName;
            _questionType = questionType;
            _submitAnswerButtonClicked = false;
            _suppressClosingWithoutSubmitingAnswerWarning = suppressClosingWithoutSubmitingAnswerWarning;
            _totalQuestionsAnsweredCorrectly = totalQuestionsAnsweredCorrectly;
            _totalQuestionsAsked = totalQuestionsAsked;

            InitializeComponent();
            InitializeTriviaDialog();
        }

        private void InitializeTriviaDialog()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var iconUri = new TriviaMessage().GetIconUri(_appName);
            Icon = new BitmapImage(iconUri);
        }

        private void AppHyperlink1_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void AppBtnRevealAnswer_OnClick(object sender, RoutedEventArgs e)
        {
            AppBtnRevealAnswer.Visibility = Visibility.Collapsed;
            AppTextBlockAnswer.Visibility = Visibility.Visible;
        }

        private void AppBtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            var shouldClose = true;

            if (_questionType != QuestionType.None)
            {
                if (!_submitAnswerButtonClicked)
                {
                    if (_suppressClosingWithoutSubmitingAnswerWarning.HasValue && !_suppressClosingWithoutSubmitingAnswerWarning.Value)
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

        private void AppBtnSubmitMultiChoiceAnwser_OnClick(object sender, RoutedEventArgs e)
        {
            _submitAnswerButtonClicked = true;

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

            ActOnAnswerGiven(response);
        }

        public delegate void MyEventHandler(int? totalQuestionsAsked, int? totalQuestionsAnsweredCorrectly);
        public event MyEventHandler PersistHiddenOptionsEventHandler;

        private void ActOnAnswerGiven(string response)
        {
            QuizReplyEmoticonCorrect.Visibility = Visibility.Collapsed;
            QuizReplyEmoticonIncorrect.Visibility = Visibility.Collapsed;

            var isResponseCorrect = IsResponseCorrect(response);
            
            if (_totalQuestionsAsked.HasValue)
            {
                _totalQuestionsAsked++;//gregt only do this once, not every time they hit submit
            }

            if (isResponseCorrect)
            {
                TextBlockQuizReply.Text = "Correct - well done, top of the class !";
                SetQuizReplyColour(Colors.Green);
                QuizReplyEmoticonCorrect.Visibility = Visibility.Visible;

                if (_totalQuestionsAnsweredCorrectly.HasValue)
                {
                    _totalQuestionsAnsweredCorrectly++;//gregt only do this once, not every time they hit submit
                }
            }
            else
            {
                if (response == null)
                {
                    TextBlockQuizReply.Text = "Nice try, but you've can't cheat and avoid supplying an answer";
                    SetQuizReplyColour(Colors.Orange);
                }
                else
                {
                    if (_questionType == QuestionType.TrueFalse)
                    {
                        TextBlockQuizReply.Text = $"Wrong - opps, must try harder !";
                    }
                    else
                    {
                        TextBlockQuizReply.Text = $"Wrong - opps, must try harder ! The correct answer is {_correctAnswer}";
                    }

                    SetQuizReplyColour(Colors.Red);
                    QuizReplyEmoticonIncorrect.Visibility = Visibility.Visible;
                }
            }

            TextBlockQuizReply.Visibility = Visibility.Visible;

            PersistHiddenOptionsEventHandler?.Invoke(_totalQuestionsAsked, _totalQuestionsAnsweredCorrectly);
        }

        private void SetQuizReplyColour(Color color)
        {
            TextBlockQuizReply.Foreground = new SolidColorBrush(color);
        }

        private bool IsResponseCorrect(string response)
        {
            var rightAnswer = response == _correctAnswer;

            return rightAnswer;
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
    }
}