using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using Trivial.Api.Gateway.AppModels;
using Trivial.Entities;

namespace Trivial.Api.Gateway
{
    public class ClientGateway
    {
        //gregtlo F1 help
        //gregtlo Reduce ide popup size

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

        public GatewayResponse GetGatewayResponse(AppName appName, int timeOutInMilliSeconds)
        {
            var url = GetUrl(appName);

            var gatewayResponse = new GatewayResponse();

            if (!string.IsNullOrEmpty(url))
            {
                var responseDto = GetRestResponse(url, timeOutInMilliSeconds);

                if (!string.IsNullOrEmpty(responseDto.ErrorDetails))
                {
                    SetGatewayResponseFromErrorDetails(gatewayResponse, responseDto.ErrorDetails);
                }
                else
                {
                    switch (appName)
                    {
                        case AppName.NumericTrivia:
                            gatewayResponse = SetGatewayResponseFromRestResponse(responseDto.ResponseContent);
                            break;
                        case AppName.TrumpQuotes:
                            gatewayResponse = SetGatewayResponseFromRestResponseTrump(responseDto.ResponseContent);
                            break;
                    }
                }
            }

            return gatewayResponse;
        }

        private static void SetGatewayResponseFromErrorDetails(GatewayResponse gatewayResponse, string errorDetails)
        {
            gatewayResponse.Text = errorDetails;
        }

        private ResponseDto GetRestResponse(string url, int timeOutInMilliSeconds)
        {
            var responseDto = new ResponseDto();

            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET) {Timeout = timeOutInMilliSeconds };
                var response = client.Execute(request);

                var errorHasOccured = response.ErrorException != null || !string.IsNullOrEmpty(response.ErrorMessage);//gregt unit test reqd

                if (errorHasOccured)
                {
                    //gregt unit test reqd
                    var errorDetails =
                        "ResponseStatus=" + response.ResponseStatus + Environment.NewLine +
                        "HttpStatusCode = " + response.StatusCode + "(" + (int)response.StatusCode + ")" + Environment.NewLine +
                        "StatusDescription=" + response.StatusDescription + Environment.NewLine +
                        "ErrorMessage=" + response.ErrorMessage + Environment.NewLine;

                    //gregt unit test reqd
                    if (response.ErrorException != null)
                    {
                        errorDetails =
                            errorDetails + Environment.NewLine +
                            "ErrorExceptionMessage=" + response.ErrorException.Message + Environment.NewLine +
                            "ErrorExceptionTargetSite=" + response.ErrorException.TargetSite;
                    }

                    responseDto.ErrorDetails = errorDetails;
                }
                else
                {
                    responseDto.ResponseContent = response.Content;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                responseDto.ErrorDetails = $"Error occured. Possible communication error with {url}";
            }

            return responseDto;
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
                    //url = "https://apixxx.xxxtronalddump.io/random/quote";
                    //url = "http://localhost:52327/Api/Values";
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

            gatewayResponse.Attribution = "Donald J. Trump, " + rootObject.appeared_at.Date.ToShortDateString();
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