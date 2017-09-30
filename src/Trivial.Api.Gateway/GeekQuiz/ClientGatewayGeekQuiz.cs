using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Trivial.Api.Gateway.GeekQuiz
{
    public class ClientGatewayGeekQuiz
    {
        public static GatewayResponseGeekQuiz SetGatewayResponseFromRestResponse(string responseContent)
        {
            var rootObject = JsonConvert.DeserializeObject<List<GeekQuizRootObject>>(responseContent);
            var gatewayResponse = GetGatewayResponse(rootObject.First());
            return gatewayResponse;
        }

        private static GatewayResponseGeekQuiz GetGatewayResponse(GeekQuizRootObject rootObject)
        {
            var firstOfOne = rootObject.results[0];

            var answer = firstOfOne.correct_answer;
            var multipleChoiceAnswers = firstOfOne.incorrect_answers;
            var difficultyLevel = firstOfOne.difficulty;
            var question = firstOfOne.question;

            var gatewayResponse = new GatewayResponseGeekQuiz
            {
                Answer = answer,
                DifficultyLevel = difficultyLevel,
                MultipleChoiceAnswers= multipleChoiceAnswers,
                Question = question,
            };

            return gatewayResponse;
        }

       
    }
}

