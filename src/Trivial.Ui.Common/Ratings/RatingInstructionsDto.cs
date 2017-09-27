using VsixRatingChaser;

namespace Trivial.Ui.Common.Ratings
{
    public class RatingInstructionsDto : IRatingInstructions
    {
        public int PackageLoadedLimit { get; set; }
        public int RatingRequestGapInDays { get; set; }
        public int RatingRequestLimit { get; set; }
        public string RatingRequestText { get; set; }
        public string RatingRequestUrl { get; set; }
    }
}