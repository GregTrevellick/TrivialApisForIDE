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
                case AppName.NumericTrivia:
                    //Consistent average response time 330ms
                    url = "http://numbersapi.com/random/trivia";
                        // YES "http://api.fixer.io/latest"
                        // YES "http://www.jservice.io/api/random"
                        // YES "https://opentdb.com/api.php?amount=1&category=18&difficulty=easy&type=multiple" //https://opentdb.com/api_config.php 
                        // YES "https://qriusity.com/v1/questions?page=2&limit=1"
                    break;
                case AppName.TrumpQuotes:
                    //Average response time 280ms but sometimes peaking around 370ms
                    url = "https://api.tronalddump.io/random/quote";
                    break;
            }

            return url;
        }
    }
}

//"https://icanhazdadjoke.com/api#fetch-a-random-dad-joke"
////"https://quotesondesign.com/api-v4-0/"
//"https://restcountries.eu/"
//PAID "http://quotes.rest/#!/quote/get_quote_random"
////https://www.coindesk.com/api/ 
////https://api.pandascore.co/rest#make-basic-requests 
////https://data.police.uk/docs/ 
////https://www.meetup.com/meetup_api/ 
////http://api.football-data.org/index