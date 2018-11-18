//using Microsoft.VisualStudio.Shell;
using System;

namespace Trivial.Ui.Jeopardy.Options
{
    public class HiddenOptions : BaseOptionModel<HiddenOptions>//: DialogPage
    {
        public DateTime LastPopUpDateTime { get; set; }
        public int PopUpCountToday { get; set; }
    }
}