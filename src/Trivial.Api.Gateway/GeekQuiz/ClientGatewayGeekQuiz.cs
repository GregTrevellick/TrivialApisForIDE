using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Trivial.Api.Gateway.GeekQuiz;

namespace Trivial.Api.Gateway.GeekQuiz
{
    public class ClientGatewayGeekQuiz
    {
        public static GatewayResponseGeekQuiz SetGatewayResponseFromRestResponse(string responseContent)
        {
            var rootObject = JsonConvert.DeserializeObject<List<Class1>>(responseContent);
            var gatewayResponse = GetGatewayResponse(rootObject.First());
            return gatewayResponse;
        }

        private static GatewayResponseGeekQuiz GetGatewayResponse(Class1 rootObject)
        {
            var answer = CharacterHandler(rootObject.answer);
            var question = CharacterHandler(rootObject.question);

            var gatewayResponse = new GatewayResponseGeekQuiz
            {
                Answer = answer,
                Question = question,
            };

            return gatewayResponse;
        }

        internal static string CharacterHandler(string str)
        {
            str = str.Replace("<i>", string.Empty);
            str = str.Replace("</i>", string.Empty);
            str = str.Replace(@"\'", "'");
            str = char.ToUpper(str[0]) + str.Substring(1);
        
            return str;
        }
    }
}

