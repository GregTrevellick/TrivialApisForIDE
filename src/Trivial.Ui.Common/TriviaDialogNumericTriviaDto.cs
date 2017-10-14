using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogNumericTriviaDto : TriviaDialogDtoBase
    {
        public TriviaDialogNumericTriviaDto(AppName appName, string optionsName, string popUpTitle, string errorDetails) : base(appName, optionsName, popUpTitle, errorDetails)
        {
        }

        public string NumericTriviaFact { get; set; }
    }
}