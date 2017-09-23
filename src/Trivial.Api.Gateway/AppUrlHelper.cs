using Trivial.Entities;

namespace Trivial.Api.Gateway
{
    public class AppUrlHelper
    {
        public static string GetUrl(AppName appName)
        {
            string url = null;

            switch (appName)
            {
                case AppName.Jeopardy://Consistent average response time 270ms
                    url = "http://www.jservice.io/api/random";
                    break;
                case AppName.NumericTrivia://Consistent average response time 330ms
                    url = "http://numbersapi.com/random/trivia";
                    break;
                case AppName.TrumpQuotes://Average response time 280ms but sometimes peaking around 370ms
                    url = "https://api.tronalddump.io/random/quote";
                    break;
                //case AppName.ExchangeRates:
                //    url = "http://api.fixer.io/latest";
                //    http://api.fixer.io/latest?symbols=USD,GBP&base=GBP
                //    break;
                //case AppName.OpenTriviaDatabase:
                //    url = "https://opentdb.com/api.php?amount=1&category=18&difficulty=easy&type=multiple"; //https://opentdb.com/api_config.php
                //    break;
                //case AppName.QruisityApp:
                //    url = "https://qriusity.com/v1/questions?page=2&limit=1";
                //    break;
            }

            return url;
        }
    }
}

//"https://icanhazdadjoke.com/api#fetch-a-random-dad-joke"
//"https://restcountries.eu/"
////"https://quotesondesign.com/api-v4-0/"
////https://www.coindesk.com/api/ 
////https://api.pandascore.co/rest#make-basic-requests 
////https://data.police.uk/docs/ 
////https://www.meetup.com/meetup_api/ 
////http://api.football-data.org/index