namespace Trivial.Api.Gateway.Jeopardy
{
    public class GatewayResponseJeopardy : GatewayResponseBase
    {
        public string Answer { get; set; }
        public string AnswerRevealLabel = "Reveal answer";
        public string Text { get; set; }//gregt rename to Question
    }
}
