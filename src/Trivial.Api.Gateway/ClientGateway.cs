using Newtonsoft.Json;
using RestSharp;
using System;
using Trivial.Api.Gateway.AppModels;
using Trivial.Entities;

namespace Trivial.Api.Gateway
{
    public static class ClientGateway
    {
        //GREGT
        //F1 help
        //Reduce ide size
        //API call timeout
        //Try w/o API call - fixes dll issue? Or API call locks dll ?
        //Alpha timeout values in options

        //https://icanhazdadjoke.com/api#fetch-a-random-dad-joke 
        //https://quotesondesign.com/api-v4-0/ 
        //https://restcountries.eu/ 
        //http://quotes.rest/#!/quote/get_quote_random

        ////https://www.coindesk.com/api/ 
        ////http://fixer.io/ 
        ////http://jservice.io/ 
        ////https://opentdb.com/api_config.php 
        ////https://qriusity.com/ 
        ////https://api.pandascore.co/rest#make-basic-requests 
        ////https://data.police.uk/docs/ 
        ////https://www.meetup.com/meetup_api/ 
        ////http://api.football-data.org/index

        public static GatewayResponse GetGatewayResponse(AppName appName)
        {
            var url = GetUrl(appName);

            var gatewayResponse = new GatewayResponse {Text = string.Empty};

            if (!string.IsNullOrEmpty(url))
            {
                var restResponse = GetRestResponse(url);

                switch (appName)
                {
                    case AppName.NumericTrivia:
                        gatewayResponse = SetGatewayResponseFromRestResponse(restResponse.Content);
                        break;
                    case AppName.TrumpQuotes:
                        gatewayResponse = SetGatewayResponseFromRestResponseTrump(restResponse.Content);
                        break;
                }
            }

            return gatewayResponse;
        }

        //public async Task<SiLogVrmQuery4Results> Query(string connectionName, VRMQueryParameters parameters)
        //{
        //    try
        //    {
        //        var start = Environment.TickCount;
        //        var task = nciRepo.GetVrmQuery4(connectionName, parameters.vrms, parameters.queryTypes, parameters.softActionTypes, parameters.location);
        //        var output = await task;
        //        var stop = Environment.TickCount;
        //        Log.Info(TimingMessage, TimeSpan.FromMilliseconds(stop - start), parameters.vrms.ToLogString(), null);
        //        return output;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(FailedMessage, ex);
        //        throw;
        //    }
        //}

        private static IRestResponse GetRestResponse(string url)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response = client.Execute(request);
                //TODO gregt async up this call ? e.g. client.ExecuteAsync(request, response => { JsonConvert.DeserializeObject<TrumpRootObject>(response.Content); });

                if (response.ErrorException != null)
                {
                    var message = $"Error retrieving response from {url} - see inner exception.";
                    throw new ApplicationException(message, response.ErrorException);
                }

                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                var message = $"Error calling web service at {url} - see inner exception.";
                throw new ApplicationException(message, ex);
            }
        }

        private static string GetUrl(AppName appName)//gregt put into factory based class or project
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

        private static GatewayResponse GetGatewayResponse(TrumpRootObject rootObject)//gregt put into factory based class or project
        {
            var gatewayResponse = new GatewayResponse();

            if (rootObject._embedded != null)
            {
                var source = "";

                foreach (var elem in rootObject._embedded.source)
                {
                    source += elem.url + " ";
                    gatewayResponse.LinkUri += source;
                }
            }

            gatewayResponse.Attribution = "   Donald J. Trump, POTUS, " + rootObject.appeared_at.Date.ToShortDateString();
            gatewayResponse.LinkText = gatewayResponse.LinkUri;
            gatewayResponse.Text = "\"" + rootObject.value + "\"";

            return gatewayResponse;
        }

        private static GatewayResponse SetGatewayResponseFromRestResponse(string responseContent)
        {
            var gatewayResponse = new GatewayResponse { Text = responseContent };
            return gatewayResponse;
        }

        private static GatewayResponse SetGatewayResponseFromRestResponseTrump(string responseContent)
        {
            var trumpRootObject = JsonConvert.DeserializeObject<TrumpRootObject>(responseContent);
            var gatewayResponse = GetGatewayResponse(trumpRootObject);
            return gatewayResponse;
        }
    }
}
