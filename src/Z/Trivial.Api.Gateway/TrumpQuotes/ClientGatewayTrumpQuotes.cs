using Newtonsoft.Json;

namespace Trivial.Api.Gateway.TrumpQuotes
{
    public class ClientGatewayTrumpQuotes
    {
        public static GatewayResponseTrumpQuotes SetGatewayResponseFromRestResponse(string responseContent)
        {
            var rootObject = JsonConvert.DeserializeObject<TrumpQuotesRootObject>(responseContent);
            var gatewayResponse = GetGatewayResponse(rootObject);
            return gatewayResponse;
        }

        private static GatewayResponseTrumpQuotes GetGatewayResponse(TrumpQuotesRootObject rootObject)
        {
            var gatewayResponse = new GatewayResponseTrumpQuotes();

            if (rootObject._embedded != null)
            {
                var source = "";

                foreach (var elem in rootObject._embedded.source)
                {
                    source += elem.url + " ";
                    gatewayResponse.HyperLinkUri += source;
                }
            }

            gatewayResponse.QuotationDate = rootObject.appeared_at.Date.ToShortDateString();
            gatewayResponse.QuotationAuthor = "Donald J. Trump";
            gatewayResponse.HyperLinkText = gatewayResponse.HyperLinkUri;
            gatewayResponse.TrumpQuote = "\"" + rootObject.value + "\"";

            return gatewayResponse;
        }
    }
}