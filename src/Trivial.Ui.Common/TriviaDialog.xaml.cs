﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
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
        //// Use this constructor to provide a Help button and F1 support. gregt
        //public WpfDialogDw(string helpTopic) : base(helpTopic)
        //{
        //    InitializeComponent();
        //}
        //

        public TriviaDialog()
        {
            InitializeComponent();
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AppHyperlink1_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}