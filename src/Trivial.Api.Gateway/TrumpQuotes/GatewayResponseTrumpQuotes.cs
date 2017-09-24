namespace Trivial.Api.Gateway.TrumpQuotes
{
    public class GatewayResponseTrumpQuotes : GatewayResponseBase
    {
        public string Author = "Donald J. Trump";//gregt rename to quoteauthor
        public string Date { get; set; }//gregt rename to quote date
        public string LinkText { get; set; }//gregt rename to prefix hyperlinktext
        public string LinkUri { get; set; }//gregt rename to prefix hyperlinkuri
        public string Text { get; set; }//gregt rename to TrumpQuote
    }
}
