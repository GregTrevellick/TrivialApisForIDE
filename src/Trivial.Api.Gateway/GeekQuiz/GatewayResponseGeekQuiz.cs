namespace Trivial.Api.Gateway.GeekQuiz
{
    public class GatewayResponseGeekQuiz : GatewayResponseBase
    {
        public string Answer { get; set; }
        public string AnswerRevealLabel = "Reveal answer";
        public string Question { get; set; }
    }
}
