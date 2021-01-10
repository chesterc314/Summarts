
using System;
using System.Collections.Generic;
using RestSharp;

namespace SummArts.Helpers
{
    public class HttpClient : IHttpClient
    {
        private const int RETRY_COUNT = 3;
        private readonly RestClient _restClient;
        public HttpClient()
        {
            _restClient = new RestClient();
        }
        public IRestResponse Get(string url, ICollection<KeyValuePair<string, string>> headers)
        {
            var request = new RestRequest();
            var response = this.GetRequest(request, url, headers);
            var count = 0;
            while (!response.IsSuccessful && count < RETRY_COUNT)
            {
                response = this.GetRequest(request, url, headers);
                if (response.IsSuccessful)
                {
                    break;
                }
                ++count;
            }
            return response;
        }

        private IRestResponse GetRequest(RestRequest request, string url, ICollection<KeyValuePair<string, string>> headers)
        {
            request.AddHeaders(headers);
            _restClient.BaseUrl = new Uri(url);
            var response = _restClient.Get(request);
            return response;
        }
    }
}