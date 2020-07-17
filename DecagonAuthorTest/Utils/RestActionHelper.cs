using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DecagonAuthorTest.Utils
{
    public class RestActionHelper
    {
        public R CallRestAction<R, T>(T model, string url, string authkey = "")
        {
            if (model == null) return default(R);
            var response = default(R);
            try
            {
                var restResponse = PostRequest(url, model, authkey);

                if (restResponse.Content != null && restResponse.Content.Length > 0)
                {
                    response = JsonConvert.DeserializeObject<R>(restResponse.Content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return response;
        }

        public R CallGetAction<R>(string url, string authkey = "")
        {
            var response = default(R);
            try
            {
                var restResponse = GetRequest(url, authkey);

                if (restResponse.Content != null && restResponse.Content.Length > 0)
                {
                    response = JsonConvert.DeserializeObject<R>(restResponse.Content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return response;
        }

        public static IRestResponse PostRequest(string requestUrl, object data, string authkey)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                IRestResponse response = new RestResponse();
                var client = new RestClient(requestUrl);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var webRequest = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                if (!string.IsNullOrEmpty(authkey))
                    webRequest.AddHeader("Authorization", authkey);

                webRequest.AddHeader("content-type", "application/json");
                webRequest.AddParameter("application/json", json, ParameterType.RequestBody);

                Task.Run(async () =>
                {
                    response = await PostResponseContentAsync(client, webRequest) as RestResponse;
                }).Wait();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static IRestResponse GetRequest(string requestUrl, string authkey)
        {
            try
            {
                IRestResponse response = new RestResponse();
                var client = new RestClient(requestUrl);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var webRequest = new RestRequest(Method.GET);

                if (!string.IsNullOrEmpty(authkey))
                    webRequest.AddHeader("Authorization", authkey);

                webRequest.AddHeader("content-type", "application/json");

                Task.Run(async () =>
                {
                    response = await GetResponseContentAsync(client, webRequest) as RestResponse;
                }).Wait();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static Task<IRestResponse> PostResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
