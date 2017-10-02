using Microsoft.VisualStudio.Shell;
using System;
using VsixRatingChaser;

namespace Trivial.Ui.Options.GeekQuiz
{
    public class HiddenChaserOptions : DialogPage, IHiddenChaserOptions
    {
        public DateTime LastRatingRequest { get; set; }
        public int PackageLoadedCount { get; set; }
        public int RatingRequestCount { get; set; }
    }
}