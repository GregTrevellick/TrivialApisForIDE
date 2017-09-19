namespace Trivial.Ui.Common
{
    public static class StringExtensions
    {
        public static int ToInteger(this string str)
        {
            var isInteger = int.TryParse(str, out int x);
            return isInteger ? x : 0;
        }
    }
}
