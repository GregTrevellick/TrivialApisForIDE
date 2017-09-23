namespace Trivial.Api.Gateway
{
    public class GatewayResponse
    {
        public string Answer { get; set; }
        public string AnswerLabel = "A. ";
        public string AnswerRevealLabel = "Reveal answer";
        public string Author { get; set; }
        public string Date { get; set; }
        public string LinkText { get; set; }//gregt rename to prefix hyperlink
        public string LinkUri { get; set; }
        public string Question { get; set; }
        public string QuestionLabel = "Q.";
        public string Text { get; set; }//gregt rename ? used for trump twitter post + numeric fact
    }
}
