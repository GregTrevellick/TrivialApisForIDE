using System;

namespace Trivial.Ui.Common
{
    public static class CommonConstants
    {
        public const string CategorySubLevel = "General";
        public const string DefaultFileQuantityWarningLimit = "10";
        public static string IntegerInvalid = "Invalid integer value specified for:" + Environment.NewLine + Environment.NewLine;
        public const string PopUpIntervalInMinsOptionDetailedDescription = "Suppress popup if previous popup was less than X minutes ago descr.";
        public const string PopUpIntervalInMinsOptionLabel = "Suppress popup if previous popup was less than X minutes ago";
        //public const string ToolsOptionsNotice = "(You can change suppress this notice in Tools | Options)";
        //public static string UnexpectedError =
        //    "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
        //    "If the error persists please log a bug for this extension via the Visual Studio Marketplace at https://marketplace.visualstudio.com" + Environment.NewLine + Environment.NewLine +
        //    "Press OK to return to Visual Studio.";
    }
}
