using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Ui.Common
{
    public static class IntExt
    {
        public static string UserStatus(this int? percentageCorrect)
        {
            if (percentageCorrect > 50)
            {
                return "Genius";
            }
            else
            {
                return "PLONKA";
            }
        }
    }
}
