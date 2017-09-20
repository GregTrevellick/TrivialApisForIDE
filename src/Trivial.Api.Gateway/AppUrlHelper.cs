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
                    url = "http://numbersapi.com/random/trivia";
                    break;
                case AppName.TrumpQuotes:
                    url = "https://api.tronalddump.io/random/quote";
                    break;
            }

            return url;
        }
    }
}

//https://icanhazdadjoke.com/api#fetch-a-random-dad-joke 
//https://quotesondesign.com/api-v4-0/ 
//https://restcountries.eu/ 
//http://quotes.rest/#!/quote/get_quote_random
///http://techxposer.com/CurrencyRate/INR
////https://www.coindesk.com/api/ 
////http://fixer.io/ 
////http://jservice.io/ 
////https://opentdb.com/api_config.php 
////https://qriusity.com/ 
////https://api.pandascore.co/rest#make-basic-requests 
////https://data.police.uk/docs/ 
////https://www.meetup.com/meetup_api/ 
////http://api.football-data.org/index