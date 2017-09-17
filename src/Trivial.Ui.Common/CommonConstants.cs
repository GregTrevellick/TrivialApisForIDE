using System;

namespace Trivial.Ui.Common
{
    public static class CommonConstants
    {
        public const string CategorySubLevel = "General";
        public const string DefaultFileQuantityWarningLimit = "10";
        public const string DefaultMaximumPopUpsWeekDay = "3";
        public const string DefaultMaximumPopUpsWeekEnd = "99";
        public const string DefaultPopUpIntervalInMins = "0";
        public static string IntegerInvalid = "Invalid integer value specified for ";
        public const string MaximumPopUpsWeekDayDetailedDescription = "Maximum pop ups Mon-Fri descr.";
        public const string MaximumPopUpsWeekDayOptionLabel = "Maximum pop ups Mon-Fri day";
        public const string MaximumPopUpsWeekEndDetailedDescription = "Maximum pop ups Sat-Sun descr.";
        public const string MaximumPopUpsWeekEndOptionLabel = "Maximum pop ups Sat-Sun day";
        public const string PopUpIntervalInMinsOptionDetailedDescription = "Suppress popup if previous popup was less than X minutes ago descr.";
        public const string PopUpIntervalInMinsOptionLabel = "Suppress popup if previous popup was less than X minutes ago";
        public const string TimeOutInMilliSecondsOptionDetailedDescription = "Max time to wait for web service descr.";
        public const string TimeOutInMilliSecondsOptionLabel = "Max time to wait for web service";

        //public const string ToolsOptionsNotice = "(You can change suppress this notice in Tools | Options)";
        //public static string UnexpectedError =
        //    "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
        //    "If the error persists please log a bug for this extension via the Visual Studio Marketplace at https://marketplace.visualstudio.com" + Environment.NewLine + Environment.NewLine +
        //    "Press OK to return to Visual Studio.";
    }
}
