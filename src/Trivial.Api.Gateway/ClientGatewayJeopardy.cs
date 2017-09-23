using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Trivial.Api.Gateway.AppModels;

namespace Trivial.Api.Gateway
{
    public class ClientGatewayJeopardy
    {
        public static GatewayResponse SetGatewayResponseFromRestResponse(string responseContent)
        {
            var rootObject = JsonConvert.DeserializeObject<List<Class1>>(responseContent);
            var gatewayResponse = GetGatewayResponse(rootObject.First());
            return gatewayResponse;
        }

        private static GatewayResponse GetGatewayResponse(Class1 rootObject)
        {
            var gatewayResponse = new GatewayResponse
            {
                Answer = rootObject.answer,
                Question = rootObject.question,
            };
            return gatewayResponse;
        }
    }
}

