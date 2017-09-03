using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualStudio.Shell;

namespace Trivial.Ui.TrumpQuotes
{
    /// <summary>
    /// Interaction logic for OptionsDialogPageControl.xaml
    /// </summary>
    public partial class OptionsDialogPageControl : UserControl
    {
        public OptionsDialogPageControl()
        {
            InitializeComponent();//dll not found relates to this gregt
            //System.Uri resourceLocater = new System.Uri("/Trivial.Ui.TrumpQuotes2;component/optionsdialogpagecontrol.xaml", System.UriKind.Relative);
            //System.Windows.Application.LoadComponent(this, resourceLocater);

            mySlider.Maximum = Enum.GetNames(typeof(DayOfWeek)).Length - 1;
        }

        /// <summary>
        /// https://wpf.2000things.com/tag/slider/
        /// </summary>
        public class DayOfWeekEnumToStringValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                DayOfWeek day = (DayOfWeek)((double)value);
                return day.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
