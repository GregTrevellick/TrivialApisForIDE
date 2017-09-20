namespace Trivial.Ui.Common
{
    public static class StringExtensions
    {
        public static bool IsNonNegativeInteger(this string str)
        {
            var isInteger = int.TryParse(str, out int x);
            if (isInteger)
            {
                if (x < 0)
                {
                    isInteger = false;
                }
            }
            return isInteger;
        }

        public static bool IsWithinRecommendedTimeoutLimits(this string str)
        {
            var isInteger = int.TryParse(str, out int x);

            bool isWithinRecommendedTimeoutLimits = false;

            if (isInteger)
            {
                if (x >= CommonConstants.RecommendedMinimumTimeoutMilliSeconds && 
                    x <= CommonConstants.RecommendedMaximumTimeoutMilliSeconds)
                {
                    isWithinRecommendedTimeoutLimits = true;
                }
            }

            return isWithinRecommendedTimeoutLimits;
        }

        public static int GetAsInteger(this string str)
        {
            var isInteger = int.TryParse(str, out int x);
            return isInteger ? x : 0;
        }
    }
}
