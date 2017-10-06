using System.Collections;
using System.Collections.Generic;

namespace Trivial.Ui.Common
{
    public static class NullableIntegerExtensions
    {
        public static string UserStatusDescription(this int? percentageCorrect)
        {

            return GetWurds(percentageCorrect, wurds);
        }

        private IDictionary<int, string> wurds1;
        private IList<string> wurds2;

        private string GetWurds1(int? percentageCorrect, IList<string> wurds)
        {
            var wurds1 = new Dictionary<int, string>
            {
                {1,""},
                {2,""},
            };

            //;
            return wurds1;
        }

    }
}
