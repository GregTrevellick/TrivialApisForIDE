namespace Trivial.Ui.Common
{
    public static class StringExtensions
    {
        public static bool IsInteger(this string str) //gregtlo unit test reqd
        {
            var isInteger = int.TryParse(str, out int x);
            return isInteger;
        }

        public static int GetAsInteger(this string str) //gregtlo unit test reqd
        {
            var isInteger = int.TryParse(str, out int x);
            return isInteger ? x : 0;
        }
    }
}
