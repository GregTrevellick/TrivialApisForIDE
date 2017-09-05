using System;

namespace Trivial.Ui.Common
{
    public class GeneralOptionsDto
    {
        public int FrequencyIntervalInDays { get; set; }
        public int MaximumPopUpsPerDay { get; set; }
        public DateTime NextPopUpDueDate { get; set; }
        public int PopUpsToday { get; set; }
    }
}