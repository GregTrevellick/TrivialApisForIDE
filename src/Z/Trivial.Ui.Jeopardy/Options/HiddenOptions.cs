using Microsoft.VisualStudio.Shell;
using System;

namespace Trivial.Ui.Jeopardy.Options
{
    public class HiddenOptions : DialogPage
    {
        public DateTime LastPopUpDateTime { get; set; }
        public int PopUpCountToday { get; set; }
    }
}