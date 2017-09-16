using System;
using System.Windows.Documents;

namespace Trivial.Ui.Common
{
    public static class Formatter
    {
        public static string GetBody(string text, string attribution)
        {
            var popUpBody = text + Environment.NewLine + Environment.NewLine +
                            attribution + Environment.NewLine + Environment.NewLine;

            //var bold = new Bold(new Run("Bold Text"));
            //tb.Inlines.Add(bold);

            //var normal = new Run("Normal Text"));
            //tb.Inlines.Add(normal);

            return popUpBody;
        }
    }
}
