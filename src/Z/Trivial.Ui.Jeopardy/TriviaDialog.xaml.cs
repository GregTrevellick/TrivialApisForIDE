using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.PlatformUI;
using Trivial.Entities;

namespace Trivial.Ui.Jeopardy
{
    public partial class TriviaDialog : DialogWindow
    {
        private AppName _appName;
        private string _optionsName;

        public TriviaDialog(AppName appName, string optionsName)
        {
            _appName = appName;
            _optionsName = optionsName;
            //_userStatusTotalsIncremented = false;

            InitializeComponent();
            InitializeTriviaDialog();

            DataContext = this;

            StackPanelJeopardy.Visibility = Visibility.Visible;            
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
            Close();
        }

        private void AppBtnRevealAnswer_OnClick(object sender, RoutedEventArgs e)//Jeopardy
        {
            AppBtnRevealAnswerJeopardy.Visibility = Visibility.Collapsed;
            AppTextBlockAnswerJeopardy.Visibility = Visibility.Visible;
        }
    }
}