using Microsoft.VisualStudio.Shell;
using System;
//////////////using System.ComponentModel;
//////////////using Trivial.Ui.Common;

namespace Trivial.Ui.TrumpQuotes.Options
{
    public class PrivateOptions : DialogPage
    {
        //////////////////[Category(CommonConstants.AppInternal)]
        //////////////////[DisplayName(CommonConstants.LastPopUpDateTimeLabel)]
        //////////////////[Description(CommonConstants.LastPopUpDateTimeDescription)]
        public DateTime LastPopUpDateTime { get; set; }

        //////////////////[Category(CommonConstants.AppInternal)]
        //////////////////[DisplayName(CommonConstants.PopUpCountTodayLabel)]
        //////////////////[Description(CommonConstants.PopUpCountTodayDescription)]
        public int PopUpCountToday { get; set; }
    }
}