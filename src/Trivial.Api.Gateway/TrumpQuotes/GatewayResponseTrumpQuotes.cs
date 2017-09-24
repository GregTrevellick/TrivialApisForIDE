namespace Trivial.Api.Gateway.TrumpQuotes
{
    public class GatewayResponseTrumpQuotes : GatewayResponseBase
    {
        public string QuotationAuthor = "Donald J. Trump";
        public string QuotationDate { get; set; }
        public string HyperLinkText { get; set; }
        public string HyperLinkUri { get; set; }
        public string TrumpQuote { get; set; }
    }
}
