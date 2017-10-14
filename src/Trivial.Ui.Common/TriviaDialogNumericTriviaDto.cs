using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogNumericTriviaDto : TriviaDialogDtoBase
    {
        public TriviaDialogNumericTriviaDto(AppName appName, string optionsName, string popUpTitle) : base(appName, optionsName, popUpTitle)
        {
        }

        public string NumericTriviaFact { get; set; }
    }
}