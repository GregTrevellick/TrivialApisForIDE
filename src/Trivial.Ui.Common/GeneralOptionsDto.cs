namespace Trivial.Ui.Common
{
    public class GeneralOptionsDto : HiddenOptionsDto
    {
        public int MaximumPopUpsWeekDay { get; set; }
        public int MaximumPopUpsWeekEnd { get; set; }
        public int PopUpIntervalInMins { get; set; }
        public int TimeOutInMilliSeconds { get; set; }
    }
}