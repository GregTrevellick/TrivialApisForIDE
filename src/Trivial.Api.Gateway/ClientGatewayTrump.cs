using Newtonsoft.Json;
using Trivial.Api.Gateway.AppModels;

namespace Trivial.Api.Gateway
{
    public class ClientGatewayTrump
    {
        public static GatewayResponse SetGatewayResponseFromRestResponseTrump(string responseContent)
        {
            var trumpRootObject = JsonConvert.DeserializeObject<TrumpRootObject>(responseContent);
            var gatewayResponse = GetGatewayResponse(trumpRootObject);
            return gatewayResponse;
        }

        private static GatewayResponse GetGatewayResponse(TrumpRootObject rootObject)
        {
            var gatewayResponse = new GatewayResponse();

            if (rootObject._embedded != null)
            {
                var source = "";

                foreach (var elem in rootObject._embedded.source)
                {
                    source += elem.url + " ";
                    gatewayResponse.LinkUri += source;
                }
            }

            gatewayResponse.Date = rootObject.appeared_at.Date.ToShortDateString();
            gatewayResponse.Author = "Donald J. Trump";
            gatewayResponse.LinkText = gatewayResponse.LinkUri;
            gatewayResponse.Text = "\"" + rootObject.value + "\"";

            return gatewayResponse;
        }
    }
}