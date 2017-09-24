using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public class TriviaDialogDto 
    {
        //gregt make upper case
        public string answer { get; set; }
        public AppName appName { get; set; }
        public string attribution { get; set; }
        public string fact { get; set; }
        public string hyperLinkUri { get; set; }
        public string optionsName { get; set; }
        public string popUpTitle { get; set; }
        public string question { get; set; }
        public string quotation { get; set; }
    }
}