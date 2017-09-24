using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogDto 
    {
        public string Answer { get; set; }
        public AppName AppName { get; set; }
        public string Attribution { get; set; }
        public string Fact { get; set; }
        public string HyperLinkUri { get; set; }
        public string OptionsName { get; set; }
        public string PopUpTitle { get; set; }
        public string Question { get; set; }
        public string Quotation { get; set; }
    }
}