using System;

namespace Trivial.Ui.Common
{
    public class GeneralOptionsDto
    {
        public DateTime LastPopUpDateTime { get; set; }
        public int MaximumPopUpsWeekDay { get; set; }
        public int MaximumPopUpsWeekEnd { get; set; }
        public int PopUpCountToday { get; set; }
        public int PopUpIntervalInMins { get; set; }
    }
}