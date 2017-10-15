using System;

namespace Trivial.Ui.Common
{
    public class HiddenOptionsDto
    {
        public DateTime LastPopUpDateTime { get; set; }
        public int PopUpCountToday { get; set; }
        public int TotalQuestionsAnsweredCorrectly { get; set; }//GeekQuiz only
        public int TotalQuestionsAsked { get; set; }//GeekQuiz only
    }
}