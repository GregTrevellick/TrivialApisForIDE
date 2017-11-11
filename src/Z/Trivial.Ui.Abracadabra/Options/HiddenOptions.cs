using Microsoft.VisualStudio.Shell;
using System;

namespace Trivial.Ui.Abracadabra.Options
{
    public class HiddenOptions : DialogPage
    {
        public DateTime LastPopUpDateTime { get; set; }
        public int PopUpCountToday { get; set; }
    }
}