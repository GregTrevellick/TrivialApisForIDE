using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics;
using System.Windows;
/////////////////using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public partial class TriviaDialog : DialogWindow
    {
        private AppName _appName;
        private string _correctAnswer;
        private string _optionsName;

        public TriviaDialog(AppName appName, string optionsName)
        {
            Init(appName, optionsName, null);
        }

        public TriviaDialog(AppName appName, string optionsName, string correctAnswer)
        {
            Init(appName, optionsName, correctAnswer);
        }

        private void Init(AppName appName, string optionsName, string correctAnswer)
        {
            _appName = appName;
            _correctAnswer = correctAnswer;
            _optionsName = optionsName;

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

            //////////////////radioBtn1.Content = "True";
            //////////////////radioBtn2.Content = "False";
            //////////////////radioBtn3.Content = "Red";
            //////////////////radioBtn4.Content = "";

            //////////////////SetRadioButtonVisibility(radioBtn1);
            //////////////////SetRadioButtonVisibility(radioBtn2);
            //////////////////SetRadioButtonVisibility(radioBtn3);
            //////////////////SetRadioButtonVisibility(radioBtn4);
        }

        ////////////////////private void SetRadioButtonVisibility(RadioButton radioButton)
        ////////////////////{
        ////////////////////    if (!string.IsNullOrWhiteSpace(radioButton.Content.ToString()))
        ////////////////////    {
        ////////////////////        radioButton.Visibility = Visibility.Visible;
        ////////////////////    }
        ////////////////////}

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
            Close();
        }

        //////////////////private void AppBtnTrue_OnClick(object sender, RoutedEventArgs e)
        //////////////////{
        //////////////////    ActOnAnswerGiven(true.ToString());
        //////////////////}

        //////////////////private void AppBtnFalse_OnClick(object sender, RoutedEventArgs e)
        //////////////////{
        //////////////////    ActOnAnswerGiven(false.ToString());
        //////////////////}

        private void AppBtnSubmitMultiChoiceAnwser_OnClick(object sender, RoutedEventArgs e)
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
                            //throw error ?
                            response = null;
                        }
                    }
                }
            }

            ActOnAnswerGiven(response);
        }

        public void ActOnAnswerGiven(string response)
        {
            bool isResponseCorrect = IsResponseCorrect(response);

            if (isResponseCorrect)
            {
                //gregthi show well done / top of class 
            }
            else
            {
                //gregthi show wrong / go away
            }
        }

        private bool IsResponseCorrect(string response)
        {
            bool rightAnswer = response == _correctAnswer;

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