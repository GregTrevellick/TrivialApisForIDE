namespace Trivial.Ui.Common
{
    public static class CommonConstants
    {
        public const string CategorySubLevel = "General";
        public const string DefaultMaximumPopUpsWeekDay = "3";
        public const string DefaultMaximumPopUpsWeekEnd = "99";
        public const string DefaultPopUpIntervalInMins = "0";
        public const string MaximumPopUpsWeekDayDetailedDescription = "The maximum number of pop-up messages you will see on any one week day. Use this setting to reduce the volume of pop-up messages during the working week.";
        public const string MaximumPopUpsWeekDayOptionLabel = "Maximum week day pop-up messages";
        public const string MaximumPopUpsWeekEndDetailedDescription = "The maximum number of pop-up messages you will see on either a Saturday or a Sunday. Use this setting to increase the volume of pop-up messages in your free time.";
        public const string MaximumPopUpsWeekEndOptionLabel = "Maximum weekend pop-ups messages";
        public const string PopUpIntervalInMinsOptionDetailedDescription = "The minimum number of minutes after a pop-up message is displayed before you will see another pop-up message. Use this setting to reduce the frequency of pop-up messages.";
        public const string PopUpIntervalInMinsOptionLabel = "Minimum number of minutes between pop-up messages";
        public const string TimeOutInMilliSecondsOptionDetailedDescription = "The maximum time in milliseconds for this extension to retrieve the message data from the external source. This value is used as the timeout parameter when calling the external web service.";
        public const string TimeOutInMilliSecondsOptionLabel = "Maximum time (milliseconds) for message retrieval";

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

//gregt to be deleted
//////public static string UnexpectedError =
//////    "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
//////    "If the error persists please log a bug for this extension via the Visual Studio Marketplace at https://marketplace.visualstudio.com" + Environment.NewLine + Environment.NewLine +
//////    "Press OK to return to Visual Studio.";