using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogJeopardyDto : TriviaDialogDtoBase
    {
        public TriviaDialogJeopardyDto(AppName appName, string optionsName, string popUpTitle) : base(appName, optionsName, popUpTitle)
        {
        }

        public string JeopardyAnswer { get; set; }
        public string JeopardyAnswerRevealLabel { get; set; }
        public string JeopardyQuestion { get; set; }
    }
}