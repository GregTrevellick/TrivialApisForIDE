using System;
using System.IO;
using System.Windows.Media.Imaging;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public static class TriviaMessage
    {
        public static HiddenOptionsDto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime, int popUpCountToday, int timeOutInMilliSeconds)
        {
            HiddenOptionsDto hiddenOptionsDto = null;

            var clientGateway = new ClientGateway();
            var gatewayResponse = clientGateway.GetGatewayResponse(appName, timeOutInMilliSeconds);

            var author = gatewayResponse.Author;
            var date = gatewayResponse.Date;
            var message = gatewayResponse.Text;

            if (!string.IsNullOrEmpty(message))
            {   
                DisplayPopUpMessage(popUpTitle, message, gatewayResponse.LinkUri, date, author);
                hiddenOptionsDto = GetHiddenOptionsDto(lastPopUpDateTime, popUpCountToday);
            }

            return hiddenOptionsDto;
        }

        private static HiddenOptionsDto GetHiddenOptionsDto(DateTime lastPopUpDateTime, int popUpCountToday)
        {
            var hiddenOptionsDto = new HiddenOptionsDto();

            var baseDateTime = DateTime.Now;

            //if last pop up was yesterday, then we have gone past midnight, so this is first pop up for today
            if (lastPopUpDateTime.Date < baseDateTime.Date)
            {
                hiddenOptionsDto.PopUpCountToday = 1;
            }
            else
            {
                hiddenOptionsDto.PopUpCountToday = popUpCountToday + 1;
            }

            hiddenOptionsDto.LastPopUpDateTime = baseDateTime;

            return hiddenOptionsDto;
        }

        private static void DisplayPopUpMessage(string popUpTitle, string message, string linkUri, string date, string author)
        {
            const string spacer = " ";

            var triviaDialog = new TriviaDialog
            {
                AppTextBlockAttribution = { Text = author + spacer + date + spacer },
                AppTextBlockMessage = { Text = message },
                Title = popUpTitle,
            };



            //gregthi set this per app
            var assemblyName = "Trivial.Ui.Common";
            var imageDirectory = "Resources";
            var imageSubDirectory = "TrumpQuotes";
            var imageName = "VsixExtensionIcon_16x16.png";//gregt make the image's build action = "Resource"
            var packUri = "pack://application:,,,/" 
                + assemblyName + ";component"
                + Path.DirectorySeparatorChar + imageDirectory
                + Path.DirectorySeparatorChar + imageSubDirectory
                + Path.DirectorySeparatorChar + imageName;
            var uri = new Uri(packUri);



            triviaDialog.AppImage.Source = new BitmapImage(uri);

            if (!string.IsNullOrEmpty(linkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(linkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(linkUri);
            }

            triviaDialog.Show();
        }
    }
}


//using System;
//using System.Windows;
//using System.Windows.Media.Imaging;
//namespace WpfApp1
//{
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//            var mode = "2";
//            Uri uri = new Uri(@"c:\Users\GregoryT\documents\visual studio 2017\Projects\WpfApp1\ClassLibrary1\NewFolder1\z1VsixExtensionIcon_16x16.png");
//            //Add a project reference to "ClassLibrary1"
//            //Make the image's build action = "Resource"
//            if (mode == "1")
//            {
//                uri = new Uri("pack://application:,,,/ClassLibrary1;component/NewFolder1/z1VsixExtensionIcon_16x16.png");
//            }
//            else
//            {
//                uri = new Uri("pack://application:,,,/ClassLibrary2;component/NewFolder1/z2VsixCommandIcon_16x16.png");
//            }
//            this.AppImage.Source = new BitmapImage(uri);
//        }
//    }
//}