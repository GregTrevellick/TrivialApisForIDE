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

            var gatewayResponse = new GatewayResponseGeekQuiz
            {
                DifficultyLevel = firstOfOne.difficulty,
                MultipleChoiceAnswers = firstOfOne.incorrect_answers,
                MultipleChoiceCorrectAnswer = firstOfOne.correct_answer,
                Question = firstOfOne.question,
                QuestionType = firstOfOne.type == "boolean" ? QuestionType.TrueFalse : QuestionType.MultiChoice
            };

            return gatewayResponse;
        }       
    }
}