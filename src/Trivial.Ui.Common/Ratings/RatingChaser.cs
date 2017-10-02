using VsixRatingChaser;

namespace Trivial.Ui.Common.Ratings
{
    public class RatingChaser
    {
        public static IChaseVerdict ChaseRatings(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions)
        {
            var chaser = new Chaser();
            var chaseVerdict = chaser.Chase(hiddenChaserOptions, ratingInstructions);
            return chaseVerdict;
        }
    }
}