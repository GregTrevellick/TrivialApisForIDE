using System.Collections.Generic;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogGeekQuizDto : TriviaDialogDtoBase
    {
        public TriviaDialogGeekQuizDto(AppName appName, string optionsName, string popUpTitle) : base(appName, optionsName, popUpTitle)
        {
        }

        public string GeekQuizDifficulty { get; set; }
        public IEnumerable<string> GeekQuizMultipleChoiceAnswers { get; set; }
        public string GeekQuizMultipleChoiceCorrectAnswer { get; set; }
        public QuestionType GeekQuizQuestionType { get; set; }
        public string GeekQuizQuestion { get; set; }
    }
}