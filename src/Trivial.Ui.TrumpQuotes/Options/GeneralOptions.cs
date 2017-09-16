//gregtlo add app icon to pop up 
//gregtlo choice of open ide or open sln
//gregtlo font size

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
        private string maximumPopUpsWeekDay;
        private string maximumPopUpsWeekEnd;
        private string popUpIntervalInMins;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.MaximumPopUpsWeekDayOptionLabel)]
        [Description(CommonConstants.MaximumPopUpsWeekDayDetailedDescription)]
        public string MaximumPopUpsWeekDay 
        {
            get => string.IsNullOrEmpty(maximumPopUpsWeekDay) ? CommonConstants.DefaultMaximumPopUpsWeekDay : maximumPopUpsWeekDay;
            set
            {
                var isInteger = int.TryParse(value, out int x);
                if (isInteger)
                {
                    maximumPopUpsWeekDay = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.MaximumPopUpsWeekDayOptionLabel);
                }
            }
        }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.MaximumPopUpsWeekEndOptionLabel)]
        [Description(CommonConstants.MaximumPopUpsWeekEndDetailedDescription)]
        public string MaximumPopUpsWeekEnd 
        {
            get => string.IsNullOrEmpty(maximumPopUpsWeekEnd) ? CommonConstants.DefaultMaximumPopUpsWeekEnd : maximumPopUpsWeekEnd;
            set
            {
                var isInteger = int.TryParse(value, out int x);
                if (isInteger)
                {
                    maximumPopUpsWeekEnd = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.MaximumPopUpsWeekEndOptionLabel);
                }
            }
        }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.PopUpIntervalInMinsOptionLabel)]
        [Description(CommonConstants.PopUpIntervalInMinsOptionDetailedDescription)]
        public string PopUpIntervalInMins
        {
            get => string.IsNullOrEmpty(popUpIntervalInMins) ? CommonConstants.DefaultPopUpIntervalInMins : popUpIntervalInMins;
            set
            {
                var isInteger = int.TryParse(value, out int x);
                if (isInteger)
                {
                    popUpIntervalInMins = value;
                }
                else
                {
                    DisplayInvalidIntegerError(CommonConstants.PopUpIntervalInMinsOptionLabel);
                }
            }
        }

        private static void DisplayInvalidIntegerError(string labelName)
        {
            var constantsForAppCommon = new ConstantsForAppCommon();
            MessageBox.Show(
                constantsForAppCommon.GetInvalidInteger(labelName),
                constantsForAppCommon.GetCaption(Vsix.Name, Vsix.Version),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        internal int PopUpIntervalInMinsInt
        {
            get
            {
                var isInteger = int.TryParse(PopUpIntervalInMins, out int x);
                return isInteger ? x : 0;
            }
        }

        internal int MaximumPopUpsWeekDayInt
        {
            get
            {
                var isInteger = int.TryParse(MaximumPopUpsWeekDay, out int x);
                return isInteger ? x : 0;
            }
        }

        internal int MaximumPopUpsWeekEndInt
        {
            get
            {
                var isInteger = int.TryParse(MaximumPopUpsWeekEnd, out int x);
                return isInteger ? x : 0;
            }
        }

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
