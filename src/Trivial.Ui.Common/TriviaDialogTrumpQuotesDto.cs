using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogTrumpQuotesDto : TriviaDialogDtoBase
    {
        public TriviaDialogTrumpQuotesDto(AppName appName, string optionsName, string popUpTitle, string errorDetails) : base(appName, optionsName, popUpTitle, errorDetails)
        {           
        }

        public string TrumpQuotesAttribution { get; set; }
        public string TrumpQuotesHyperLinkUri { get; set; }
        public string TrumpQuotesQuotation { get; set; }
    }
}