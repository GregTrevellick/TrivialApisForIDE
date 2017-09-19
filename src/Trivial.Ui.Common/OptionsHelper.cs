namespace Trivial.Ui.Common
{
    public static class OptionsHelper
    {
        public static bool IsInteger(string value)
        {
            var isInteger = int.TryParse(value, out int x);
            return isInteger;
        }
    }
}
