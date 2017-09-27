using Microsoft.VisualStudio.Shell;
using System;
using VsixRatingChaser;

namespace Trivial.Ui.Jeopardy.Options
{
    public class HiddenRatingChaserOptions : DialogPage, IHiddenRatingChaserOptions
    {
        public DateTime LastRatingRequest { get; set; }
        public int PackageLoadedCount { get; set; }
        public int RatingRequestCount { get; set; }
    }
}