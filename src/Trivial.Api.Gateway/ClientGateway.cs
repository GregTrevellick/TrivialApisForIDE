﻿using RestSharp;
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

        private static ResponseDto GetRestResponse(string url, int timeOutInMilliSeconds)
        {
            var responseDto = new ResponseDto();

            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET) {Timeout = timeOutInMilliSeconds };
                var response = client.Execute(request);

                var hasErrorOccured = HasErrorOccured(response);
                if (hasErrorOccured)
                {
                    responseDto.ErrorDetails = GetErrorDetails(response);
                }
                else
                {
                    responseDto.ResponseContent = response.Content;
                }
            }
            catch(Exception ex)
            {
                HandleUnexpectedError(url, ex, responseDto);
            }

            return responseDto;
        }

        private static void HandleUnexpectedError(string url, Exception ex, ResponseDto responseDto)
        {
            Debug.WriteLine(ex.Message);
            var exceptionTypeName = ex.GetType().Name;
            responseDto.ErrorDetails = $"An unexpected error of type {exceptionTypeName} has occured (possibly a communication error with {url}).";
        }

        internal static bool HasErrorOccured(IRestResponse response)
        {
            var errorHasOccured =
                response == null ||
                response.ErrorException != null ||
                !string.IsNullOrEmpty(response.ErrorMessage);

            return errorHasOccured;
        }

        private static string GetErrorDetails(IRestResponse response)
        {
            string errorDetails;

            if (response == null)
            {
                errorDetails = "An indeterminate error has occured";
            }
            else
            {
                errorDetails =
                    response.ResponseStatus + " " +
                    "(" + response.StatusCode + ") " +
                    response.StatusDescription + " " +
                    response.ErrorMessage + " ";

                if (response.ErrorException != null &&
                    response.ErrorException.Message != response.ErrorMessage)
                {
                    errorDetails = errorDetails + " " + response.ErrorException.Message;
                }
            }

            return errorDetails;
        }

        private static void SetGatewayResponseFromErrorDetails(GatewayResponse gatewayResponse, string errorDetails)
        {
            gatewayResponse.Text = errorDetails;
        }

        private static GatewayResponse SetGatewayResponseFromRestResponse(string responseContent)
        {
            var gatewayResponse = new GatewayResponse { Text = responseContent };
            return gatewayResponse;
        }
    }
}