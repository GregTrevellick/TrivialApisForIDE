using System;

namespace Trivial.Ui.Common
{
    public static class Formatter
    {
        public static string GetBody(string text, string attribution)
        {
            var popUpBody = text + Environment.NewLine + Environment.NewLine +
                            attribution + Environment.NewLine + Environment.NewLine;

            return popUpBody;
        }
    }
}
