using System.Collections;
using System.Collections.Generic;

namespace Trivial.Ui.Common
{
    public static class NullableIntegerExtensions
    {
        public static string UserStatusDescription(this int? percentageCorrect)
        {

            return GetWurds(percentageCorrect, theWurd);
        }

        //private IDictionary<int, string> wurds1;
        private IList<string> wurds2;

        private IDictionary<int, string> Wurds1
        {
            get
            {
                return new Dictionary<int, string>
                {
                    {1, "back to school"},
                    {2, "homer"},
                    {3, "fail"},
                    {4, "bart"},
                    {5, "E minus"},
                    {6, "donkey "},
                    {7, "doofus "},
                    {8, "dork"},
                    {9, "nincompoop "},
                    {10, "doughnut"},
                    {11, "dummy"},
                    {12, "dunce"},
                    {13, "goon "},
                    {14, "nitwit "},
                    {15, "half - wit "},
                    {16, "nugget"},
                    {17, "turkey"},
                    {18, "poor show"},
                    {19, "numbskull"},
                    {20, "must try harder"},
                    {21, "blockhead "},
                    {22, "birdbrain "},
                    {23, "thick"},
                    {24, "thick"},
                    {25, "thick"},
                    {26, "thick"},
                    {27, "thick"},
                    {28, "thick"},
                    {29, "thick"},
                    {30, "thick"},
                    {31, "thick"},
                    {32, "thick"},
                    {33, "beginner"},
                    {34, "apprentice"},
                    {35, "novice"},
                    {36, "average"},
                    {37, "standard"},
                    {38, "run - of - the - mill"},
                    {39, "unexceptional"},
                    {40, "ordinary"},
                    {41, "passable"},
                    {42, "regular"},
                    {43, "respectable"},
                    {44, "modest"},
                    {45, "normal"},
                    {46, "bravo"},
                    {47, "decent"},
                    {48, "bright"},
                    {49, "Comic book guy"},
                    {50, "nerd"},
                    {51, "average"},
                    {52, "average"},
                    {53, "average"},
                    {54, "average"},
                    {55, "average"},
                    {56, "average"},
                    {57, "average"},
                    {58, "average"},
                    {59, "average"},
                    {60, "average"},
                    {61, "average"},
                    {62, "average"},
                    {63, "average"},
                    {64, "average"},
                    {65, "average"},
                    {66, "brainy"},
                    {67, "brainy"},
                    {68, "brainy"},
                    {69, "brainy"},
                    {70, "brainy"},
                    {71, "brainy"},
                    {72, "quiz master"},
                    {73, "millionaire"},
                    {74, "sage"},
                    {75, "whiz"},
                    {76, "wow"},
                    {77, "ace"},
                    {78, "brain box"},
                    {79, "brainiac"},
                    {80, "A plus"},
                    {81, "billionaire"},
                    {82, "Lisa Simpson"},
                    {83, "wunderbar"},
                    {84, "IQ buster"},
                    {85, "mega"},
                    {86, "liskov"},
                    {87, "cerebral"},
                    {88, "egg head"},
                    {89, "skeet"},
                    {90, "rocket scientist"},
                    {91, "top of the class"},
                    {92, "turing"},
                    {93, "babbage"},
                    {94, "exceptional"},
                    {95, "lovelace(ada)"},
                    {96, "expert"},
                    {97, "einstein"},
                    {98, "10 out of 10"},
                    {99, "11 out of 10"},
                    {100, "genius"},
                };
            }
        }

    }
}
