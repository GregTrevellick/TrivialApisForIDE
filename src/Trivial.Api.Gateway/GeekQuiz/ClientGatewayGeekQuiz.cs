using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Trivial.Api.Gateway.GeekQuiz
{
    public class ClientGatewayGeekQuiz
    {
        public static GatewayResponseGeekQuiz SetGatewayResponseFromRestResponse(string responseContent)
        {
            var rootObject = JsonConvert.DeserializeObject<GeekQuizRootObject>(responseContent);
            var gatewayResponse = GetGatewayResponse(rootObject);
            return gatewayResponse;
        }

        private static GatewayResponseGeekQuiz GetGatewayResponse(GeekQuizRootObject rootObject)
        {
            var firstOfOne = rootObject.results.First();

            var multipleChoiceCorrectAnswer = firstOfOne.correct_answer;
            var multipleChoiceCorrectAnswerAsCollection = new List<string> {multipleChoiceCorrectAnswer};
            var multipleChoiceAnswers = multipleChoiceCorrectAnswerAsCollection.Union(firstOfOne.incorrect_answers);

            var gatewayResponse = new GatewayResponseGeekQuiz
            {
                DifficultyLevel = firstOfOne.difficulty,
                MultipleChoiceAnswers = multipleChoiceAnswers,
                MultipleChoiceCorrectAnswer = multipleChoiceCorrectAnswer,
                Question = firstOfOne.question,
                QuestionType = firstOfOne.type == "boolean" ? QuestionType.TrueFalse : QuestionType.MultiChoice
            };

            return gatewayResponse;
        }       
    }
}