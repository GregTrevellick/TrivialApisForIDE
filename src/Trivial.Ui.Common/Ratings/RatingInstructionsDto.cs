using VsixRatingChaser;

namespace Trivial.Ui.Common.Ratings
{
    public class RatingInstructionsDto : IRatingInstructions
    {
        public AggressionLevel AggressionLevel { get; set; }
        public byte[] ImageByteArray { get; set; }
        public CostCategory CostCategory { get; set; }
        public DialogType DialogType { get; set; }
        ///////////////////////////////////////////////////////public int PackageLoadedLimit { get; set; }
        public string VsixAuthor { get; set; }
        public string VsixName { get; set; }
    }
}