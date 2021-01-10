
using System.Collections.Generic;
using RestSharp;

namespace SummArts.Helpers
{
    public interface IHttpClient
    {
        IRestResponse Get(string url, ICollection<KeyValuePair<string, string>> headers);
    }
}