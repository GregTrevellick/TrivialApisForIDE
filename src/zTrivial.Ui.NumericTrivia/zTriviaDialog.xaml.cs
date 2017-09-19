using System.Windows;
using Microsoft.VisualStudio.PlatformUI;

namespace Ui.NumericTrivia
{
    public partial class TriviaDialog : DialogWindow
    {
        //// Use this constructor to provide a Help button and F1 support.
        //public WpfDialogDw(string helpTopic) : base(helpTopic)
        //{
        //    InitializeComponent();
        //}

        public TriviaDialog()
        {
            InitializeComponent();

            this.SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
    }
}