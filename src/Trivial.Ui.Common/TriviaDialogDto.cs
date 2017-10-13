using System.Collections.Generic;
using Trivial.Api.Gateway;
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

    public class TriviaDialogNumericTriviaDto : TriviaDialogDtoBase
    {
        public TriviaDialogNumericTriviaDto(AppName appName, string optionsName, string popUpTitle) : base(appName, optionsName, popUpTitle)
        {
        }

        public string NumericTriviaFact { get; set; }
    }

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

    public class TriviaDialogTrumpQuotesDto : TriviaDialogDtoBase
    {
        public TriviaDialogTrumpQuotesDto(AppName appName, string optionsName, string popUpTitle) : base(appName, optionsName, popUpTitle)
        {           
        }

        public string TrumpQuotesAttribution { get; set; }
        public string TrumpQuotesHyperLinkUri { get; set; }
        public string TrumpQuotesQuotation { get; set; }
    }

    public class TriviaDialogDtoBase
    {
        public TriviaDialogDtoBase(AppName appName, string optionsName, string popUpTitle)
        {
            AppName = appName;
            OptionsName = optionsName;
            PopUpTitle = popUpTitle;
        }

        public AppName AppName { get; set; }
        public string ErrorDetails { get; set; }
        public string OptionsName { get; set; }
        public string PopUpTitle { get; set; }
    }
}