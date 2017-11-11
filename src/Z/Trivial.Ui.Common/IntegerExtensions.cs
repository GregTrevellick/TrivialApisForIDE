using System.Collections.Generic;
using System.Linq;

namespace Trivial.Ui.Common
{
    public static class IntegerExtensions
    {
        public static string UserStatusDescription(this int percentageCorrect)
        {
            return Ratings.Single(x=>x.Key == percentageCorrect).Value;
        }

        private static IDictionary<int, string> Ratings
        {
            get
            {
                return new Dictionary<int, string>
                {
                    {0, "rubbish"},
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
                    {23, "Abnormal"},
                    {24, "Awful"},
                    {25, "Rainy"},
                    {26, "Dull"},
                    {27, "Dreadful"},
                    {28, "Dismal"},
                    {29, "Toilet"},
                    {30, "thick30"},
                    {31, "thick31"},
                    {32, "thick32"},
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
                    {51, "Basic"},
                    {52, "Adequate"},
                    {53, "Pedestrian"},
                    {54, "Amateur"},
                    {55, "Bog STD"},
                    {56, "Silver"},
                    {57, "Bronze"},
                    {58, "Lightweight"},
                    {59, "Middle class"},
                    {60, "average60"},
                    {61, "average61"},
                    {62, "average62"},
                    {63, "average63"},
                    {64, "average64"},
                    {65, "average65"},
                    {66, "Gold medal"},
                    {67, "Silver"},
                    {68, "Bronze"},
                    {69, "Olympic"},
                    {70, "No 1"},
                    {71, "Superb"},
                    //Top notch
                    //Unbelievable
                    //World champ
                    //Hole in one
                    //Admirable
                    //Regal
                    //Deluxe
                    //Rolls Royce
                    //Champagne
                    //Sunny
                    //Hot
                    //Diamond
                    //Platinum
                    //Heavyweight
                    //Watson
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
