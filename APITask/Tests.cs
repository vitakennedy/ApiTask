using System.Net;
using APITask.CreateHttpClient;
using Newtonsoft.Json;
using NUnit.Framework;

namespace APITask;

[TestFixture]
public class Tests
{
    private string url;
    private HttpClient client;
    private HttpResponseMessage response;

   [SetUp]
    public void SetUp()
    {
         url = "https://jsonplaceholder.typicode.com/";
         client = GetClient.CreateHttpClient(url);
    }

    [Test]
    public void ResponseIsOk()
    {
        var responseStatusCode = GetUsers().StatusCode;
        Assert.That(responseStatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void CheckCountOfUser()
    {
        var responseContent = GetUsers().Content.ReadAsStringAsync().Result;
        var users = JsonConvert.DeserializeObject<List<MyArray>>(responseContent);
        Assert.That(users, Has.Count.EqualTo(10));
    }

    [Test]
    public void CheckHeaderContainsContentType()
    {
        var headers = GetUsers().Content.Headers.ToList();
        var key = headers.Any(x=>x.Key=="Content-Type");
        Assert.IsTrue(key);
    }

    [Test]
    public void CheckContentTypeHeaderValue()
    {
        var contentTypeValue = GetUsers().Content.Headers.ContentType.ToString();
        Assert.That(contentTypeValue, Is.EqualTo("application/json; charset=utf-8"));
    }

    public HttpResponseMessage GetUsers() => client.GetAsync("users").Result;
}