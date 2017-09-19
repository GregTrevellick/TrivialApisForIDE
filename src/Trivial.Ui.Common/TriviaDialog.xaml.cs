using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace Trivial.Ui.Common
{
    public partial class TriviaDialog : DialogWindow
    {
        public TriviaDialog()
        {
            InitializeComponent();
            InitializeTriviaDialog();
        }

        private void InitializeTriviaDialog()
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HasMaximizeButton = true;
            HasMinimizeButton = true;
        }

        private void AppHyperlink1_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void AppBtn1_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}