using System;

namespace Trivial.Ui.Common
{
    public static class CommonConstants
    {
        public const int RecommendedMaximumTimeoutMilliSeconds = 3000;
        public const int RecommendedMinimumTimeoutMilliSeconds = 400;
        public const string CategorySubLevel = "General";
        public const string DefaultMaximumPopUpsWeekDay = "7";
        public const string DefaultMaximumPopUpsWeekEnd = "99";
        public const string DefaultPopUpIntervalInMins = "0";
        public const string DefaultTimeOutInMilliSeconds = "1000";
        public const string MaximumPopUpsWeekDayDetailedDescription = "The maximum number of pop-up messages you will see on any one week day. Use this setting to reduce the volume of pop-up messages during the working week.";
        public const string MaximumPopUpsWeekDayOptionLabel = "Daily pop-up limit (midweek)";
        public const string MaximumPopUpsWeekEndDetailedDescription = "The maximum number of pop-up messages you will see on either a Saturday or a Sunday. Use this setting to increase the volume of pop-up messages in your free time.";
        public const string MaximumPopUpsWeekEndOptionLabel = "Daily pop-up limit (weekend)";
        public const string PopUpIntervalInMinsOptionDetailedDescription = "Use this setting to reduce the overall frequency with which you see pop-up messages (especially if you open a lot of solution files in a short time frame). This is the minimum number of minutes after a pop-up message is displayed before you will see another pop-up message.";
        public const string PopUpIntervalInMinsOptionLabel = "Number of minutes between pop-ups";
        public const string TimeOutInMilliSecondsOptionDetailedDescription = "The maximum time in milliseconds for this extension to spend retrieving the pop-up message data from the third party data source. This value is used as the timeout parameter when calling the external web service. A value of zero is ignored by the external web service call and will not cause an instant timeout.";
        public const string TimeOutInMilliSecondsOptionLabel = "Timeout limit (milliseconds)";
        public static string TimeOutInMilliSecondsIsOutsideRecommendedTimeoutLimits = $"Are you sure ?{Environment.NewLine}{Environment.NewLine}The value for '{TimeOutInMilliSecondsOptionLabel}' is outside the recommended limits, which means the data retrieval may timeout before completion or may wait for a long time to complete.";

       ////////////////////////////////////////////////////// public const int PackageLoadedLimit = 1;//1000
       //////////////////////////////////////////////////////////// public const int RatingRequestGap = 1;//90
       //////////////////////////////////////////////////// public const int RatingRequestLimit = 99999;//3
       ////////////////////////////////////////////// public const string RatingRequestText = "This is free, please rate it as payment by clicking here";

        public static string GetCaption(string vsixName, string vsixVersion)
        {
            return $"{vsixName} {vsixVersion}";
        }

        public static string GetInvalidInteger(string labelName)
        {
            return $"Invalid integer value specified for '{labelName}'";
        }
    }
}