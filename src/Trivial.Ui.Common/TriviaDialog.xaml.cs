using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics;
using System.Windows;
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
            Close();
        }

        private void AppBtnTrue_OnClick(object sender, RoutedEventArgs e)
        {
            ActOnAnswerGiven(true.ToString());
        }

        private void AppBtnFalse_OnClick(object sender, RoutedEventArgs e)
        {
            ActOnAnswerGiven(false.ToString());
        }

        //gregthi add radio buttons to ui & implement this
        //private void AppMultiChoiceSelection_OnClick(object sender, RoutedEventArgs e)
        //{
        //    //accept response
        //    ActOnAnswerGiven(response);
        //}

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