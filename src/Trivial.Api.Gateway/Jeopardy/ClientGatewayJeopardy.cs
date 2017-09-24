using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Trivial.Api.Gateway.Jeopardy
{
    public class ClientGatewayJeopardy
    {
        public static GatewayResponseJeopardy SetGatewayResponseFromRestResponse(string responseContent)
        {
            var rootObject = JsonConvert.DeserializeObject<List<Class1>>(responseContent);
            var gatewayResponse = GetGatewayResponse(rootObject.First());
            return gatewayResponse;
        }

        private static GatewayResponseJeopardy GetGatewayResponse(Class1 rootObject)
        {
            var gatewayResponse = new GatewayResponseJeopardy
            {
                Answer = rootObject.answer,
                Text = rootObject.question,
            };
            return gatewayResponse;
        }
    }
}

