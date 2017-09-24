namespace Trivial.Api.Gateway.NumericTrivia
{
    public class ClientGatewayNumericTrivia
    {
        public static GatewayResponseNumericTrivia SetGatewayResponseFromRestResponse(string responseContent)
        {
            var gatewayResponse = new GatewayResponseNumericTrivia
            {
                Text = responseContent,
            };
            return gatewayResponse;
        }
    }
}