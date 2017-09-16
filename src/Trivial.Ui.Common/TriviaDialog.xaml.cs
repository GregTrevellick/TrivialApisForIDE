﻿using System.Diagnostics;
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

        public TriviaDialog()
        {
            Init();
        }

        private void Init()
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
            AppTextBlock3.Text = "Change frequency of delivery in Tools > Options > " + this.Title;
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