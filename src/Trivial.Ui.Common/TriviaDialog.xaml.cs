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
        //// Use this constructor to provide a Help button and F1 support.
        //public TriviaDialog(string helpTopic) : base(helpTopic)
        //{
        //    Init();
        //}

        public TriviaDialog(string optionsName)
        {
            Init(optionsName);
        }

        private void Init(string optionsName)
        {
            InitializeComponent();
            InitializeTriviaDialog(optionsName);
        }

        private void InitializeTriviaDialog(string optionsName)
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            AppTextBlock3.Text = "Change frequency of pop-up delivery in Tools > Options > " + optionsName;
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