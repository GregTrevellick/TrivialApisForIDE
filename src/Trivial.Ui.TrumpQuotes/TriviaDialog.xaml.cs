using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Trivial.Entities;

namespace Trivial.Ui.TrumpQuotes
{
    public partial class TriviaDialog : DialogWindow
    {
        private AppName _appName;
        private string _optionsName;

        public TriviaDialog(AppName appName, string optionsName)
        {
            _appName = appName;
            _optionsName = optionsName;

            InitializeComponent();
            InitializeTriviaDialog();

            DataContext = this;

            StackPanelTrumpQuotes.Visibility = Visibility.Visible;
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

        private void AppHyperlink1_RequestNavigate(object sender, RequestNavigateEventArgs e)//TrumpQuotes
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}