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
        public TriviaDialog(AppName appName)
        {
            InitializeComponent();
            InitializeTriviaDialog(appName);
        }

        private void InitializeTriviaDialog(AppName appName)
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var iconUri = TriviaMessage.GetIconUri(appName);
            Icon = new BitmapImage(iconUri);
        }

        private void AppHyperlink1_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void AppBtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AppBtnHelp_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockHelp.Text = "To alter frequency and volume of delivery go to Tools | Options | Drain The Swamp";
            TextBlockHelp.Visibility = Visibility.Visible;
        }        
    }
}