using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogDtoBase
    {
        public TriviaDialogDtoBase(AppName appName, string optionsName, string popUpTitle, string errorDetails)
        {
            AppName = appName;
            ErrorDetails = errorDetails;
            OptionsName = optionsName;
            PopUpTitle = popUpTitle;
        }

        public AppName AppName { get; set; }
        public string ErrorDetails { get; set; }
        public string OptionsName { get; set; }
        public string PopUpTitle { get; set; }
    }
}