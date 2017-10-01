using System.Collections.Generic;

namespace Trivial.Api.Gateway.GeekQuiz
{
    public class GatewayResponseGeekQuiz : GatewayResponseBase
    {
        public string MultipleChoiceCorrectAnswer { get; set; }
       // public string AnswerRevealLabel = "Reveal answer";
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public string DifficultyLevel { get; set; }
        public IEnumerable<string> MultipleChoiceAnswers { get; set; }       
    }
}
