using System.Collections.Generic;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogDto 
    {
        public AppName AppName { get; set; }       
        public string Answer { get; set; }
        public string AnswerRevealLabel { get; set; }
        public string Attribution { get; set; }
        public string DifficultyLevel { get; set; }
        public string ErrorDetails { get; set; }
        public string Fact { get; set; }
        public string HyperLinkUri { get; set; }
        public IEnumerable<string> MultipleChoiceAnswers { get; set; }
        public string MultipleChoiceCorrectAnswer { get; set; }
        public string OptionsName { get; set; }
        public string PopUpTitle { get; set; }
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Quotation { get; set; }
    }
}