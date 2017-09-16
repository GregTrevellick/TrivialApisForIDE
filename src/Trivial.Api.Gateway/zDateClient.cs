//using RestSharp;
//using System;
//using System.Threading.Tasks;

//namespace Trivial.Api.Gateway
//{
//    public class DateClient : ClientBase, IRestClient
//    {
//        public DateClient() : base("Constants.BaseApiUrl") { }

//        public DateTime? GetDate()
//        {
//            var request = new RestRequest("/home/date");
//            request.Timeout = 50;
//            return Execute<DateTime?>(request).Data;
//        }

//        public async Task<DateTime?> GetDateAsync()
//        {
//            var request = new RestRequest("/home/date");
//            request.Timeout = 50;
//            var result = await ExecuteTaskAsync<DateTime?>(request);
//            return result.Data;
//        }
//    }
//}