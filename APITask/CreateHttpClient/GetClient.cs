using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITask.CreateHttpClient;
public static class GetClient
{
    private static string url;

    public static HttpClient CreateHttpClient(string url)
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri(url)
        };

        return client;
    }
}
