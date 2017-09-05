//gregt future - options 
//02 frequency of delivery
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
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("FrequencyInterval")]
        [Description("FrequencyInterval descr.")]
        public int FrequencyInterval { get; set; } = 1;

        internal DateTime NextPopUpDueDate { get; set; } = DateTime.Now;

        protected override void OnApply(PageApplyEventArgs e)
        {
            VSPackage.Options.NextPopUpDueDate = DateTime.Now.AddDays(FrequencyInterval);
            VSPackage.Options.SaveSettingsToStorage();

            base.OnApply(e);
        }

        //public override void LoadSettingsFromStorage()
        //{
        //    base.LoadSettingsFromStorage();
        //}
    }
}