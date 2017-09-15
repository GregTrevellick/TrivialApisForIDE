//gregt future - options 
//03 specify web service timeout
//50 add app icon to pop up 
//97 choice of open ide or open sln
//98 choice of model dialog or simple dialog
//99 font size

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Trivial.Ui.Common;

namespace Trivial.Ui.TrumpQuotes.Options
{
    public class GeneralOptions : DialogPage
    {
        internal DateTime LastPopUpDateTime { get; set; } = DateTime.Now;
        internal int PopUpCountToday { get; set; }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Suppress popup if previous popup was less than X minutes ago")]
        [Description("Suppress popup if previous popup was less than X minutes ago descr.")]
        public int PopUpIntervalInMins { get; set; } = 0;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Maximum pop ups Mon-Fri day")]
        [Description("Maximum pop ups Mon-Fri descr.")]
        public int MaximumPopUpsWeekDay { get; set; } = 3;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Maximum pop ups Sat-Sun day")]
        [Description("Maximum pop ups Sat-Sun descr.")]
        public int MaximumPopUpsWeekEnd { get; set; } = int.MaxValue;

        protected override void OnApply(PageApplyEventArgs e)
        {
            VSPackage.Options.SaveSettingsToStorage();
            base.OnApply(e);
        }
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////public override void LoadSettingsFromStorage()
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    base.LoadSettingsFromStorage();
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
