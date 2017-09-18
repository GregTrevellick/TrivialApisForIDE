using RestSharp;
using System;
using System.Diagnostics;
using Trivial.Api.Gateway.AppModels;
using Trivial.Entities;

namespace Trivial.Api.Gateway
{
    public class ClientGateway
    {
        public GatewayResponse GetGatewayResponse(AppName appName, int timeOutInMilliSeconds)
        {
            var url = AppUrlHelper.GetUrl(appName);

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
                            gatewayResponse = ClientGatewayTrump.SetGatewayResponseFromRestResponseTrump(responseDto.ResponseContent);
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

                var errorHasOccured = response.ErrorException != null || !string.IsNullOrEmpty(response.ErrorMessage);//gregtlo unit test reqd

                if (errorHasOccured)
                {
                    //gregtlo unit test reqd
                    var errorDetails =
                        "ResponseStatus=" + response.ResponseStatus + Environment.NewLine +
                        "HttpStatusCode = " + response.StatusCode + "(" + (int)response.StatusCode + ")" + Environment.NewLine +
                        "StatusDescription=" + response.StatusDescription + Environment.NewLine +
                        "ErrorMessage=" + response.ErrorMessage + Environment.NewLine;

                    //gregtlo unit test reqd
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

        //private static string GetUrl(AppName appName)//gregt put into factory based class or project
        //{
        //    string url = null;

        //    switch (appName)
        //    {
        //        case AppName.NumericTrivia:
        //            url = "http://numbersapi.com/random/trivia";
        //            break;
        //        case AppName.TrumpQuotes:
        //            url = "https://api.tronalddump.io/random/quote";
        //            //url = "https://apixxx.xxxtronalddump.io/random/quote";
        //            //url = "http://localhost:52327/Api/Values";
        //            break;
        //    }

        //    return url;
        //}

        private static GatewayResponse SetGatewayResponseFromRestResponse(string responseContent)
        {
            var gatewayResponse = new GatewayResponse { Text = responseContent };
            return gatewayResponse;
        }
    }
}