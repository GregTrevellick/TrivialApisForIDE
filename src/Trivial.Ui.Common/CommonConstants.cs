using System;

namespace Trivial.Ui.Common
{
    public static class CommonConstants
    {
        public const string CategorySubLevel = "General";
        public const string DefaultFileQuantityWarningLimit = "10";
        public static string FileQuantityWarningLimitInvalid = "Invalid integer value specified for:" + Environment.NewLine + Environment.NewLine + FileQuantityWarningLimitOptionLabel;
        public const string FileQuantityWarningLimitOptionDetailedDescription = "The number of artefacts that can be opened at one time before a warning is displayed. You will be able to open artefacts whose count exceeds this number, but you will be informed that the number of artefacts is very high. This allows you to avoid accidentely opening hundreds or thousands of files artefacts may affect performance of your machine.";
        public const string FileQuantityWarningLimitOptionLabel = "Simultaneous artefacts opening count warning limit";
        //public const string ToolsOptionsNotice = "(You can change suppress this notice in Tools | Options)";
        //public static string UnexpectedError =
        //    "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
        //    "If the error persists please log a bug for this extension via the Visual Studio Marketplace at https://marketplace.visualstudio.com" + Environment.NewLine + Environment.NewLine +
        //    "Press OK to return to Visual Studio.";
    }
}
