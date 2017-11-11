namespace Trivial.Api.Gateway.Jeopardy
{
    public class GatewayResponseJeopardy : GatewayResponseBase
    {
        public string Answer { get; set; }
        public string AnswerRevealLabel = "Reveal answer";
        public string Question { get; set; }
    }
}
