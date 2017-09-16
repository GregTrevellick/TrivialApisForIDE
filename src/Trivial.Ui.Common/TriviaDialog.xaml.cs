using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.VisualStudio.PlatformUI;

namespace Trivial.Ui.Common
{
    /// <summary>
    /// Interaction logic for TriviaDialog.xaml
    /// </summary>
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
            //gregtlo any of these worthwhile ?

            //this.HasHelpButton = true;
            //this.IsCloseButtonEnabled = true;
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