using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogTrumpQuotesDto : TriviaDialogDtoBase
    {
        public TriviaDialogTrumpQuotesDto(AppName appName, string optionsName, string popUpTitle) : base(appName, optionsName, popUpTitle)
        {           
        }

        public string TrumpQuotesAttribution { get; set; }
        public string TrumpQuotesHyperLinkUri { get; set; }
        public string TrumpQuotesQuotation { get; set; }
    }
}