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
        private AppName appName;
        private string optionsName;
        private string correctAnswer;

        public TriviaDialog(AppName appName, string optionsName, string correctAnswer = null)
        {
            this.appName = appName;
            this.optionsName = optionsName;
            this.correctAnswer = correctAnswer;
            InitializeComponent();
            InitializeTriviaDialog();
        }

        private void InitializeTriviaDialog()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var iconUri = new TriviaMessage().GetIconUri(appName);
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

        //private void AppMultiChoiceSelection_OnClick(object sender, RoutedEventArgs e)
        //{
        //    //accept response
        //    var multiChoiceResponse = "A";
        //    ActOnAnswerGiven(response);
        //}

        public void ActOnAnswerGiven(string response)
        {
            bool isResponseCorrect = IsResponseCorrect(response, correctAnswer);

            isResponseCorrect = IsResponseCorrect(response, correctAnswer);

            if (isResponseCorrect)
            {
                // show well done / top of class / piss of babbage
            }
            else
            {
                // get lost / beaten by lovelace
            }
        }

        private bool IsResponseCorrect(string response, string correctAnswer)
        {
            bool rightAnswer = response == correctAnswer;

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
                TextBlockHelp.Text = $"To alter frequency and volume of delivery go to Tools | Options | {optionsName}";
                TextBlockHelp.Visibility = Visibility.Visible;
            }
        }        
    }
}