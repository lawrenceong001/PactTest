using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace consumer.tests
{
  public class ApiClient
  {
    private readonly HttpClient _client;
    public ApiClient(string baseUri = null)
    {
      _client = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://my-api") };

    }
    public async Task<HttpResponseMessage> GetSomething(int id)
    {

      string reasonPhrase;

      var request = new HttpRequestMessage(HttpMethod.Get, "/Something/" + id.ToString());
      request.Headers.Add("Accept", "application/json");

      var response = await _client.SendAsync(request);
      return response;

    }

    public async Task<Something> getResponseObject(HttpResponseMessage response)
    {
      var content = await response.Content.ReadAsStringAsync();
      var status = response.StatusCode;
      if (status == HttpStatusCode.OK)
      {
        return !String.IsNullOrEmpty(content) ?
          JsonConvert.DeserializeObject<Something>(content)
          : null;
      }
      else 
      {
        return !String.IsNullOrEmpty(content) ?
          JsonConvert.DeserializeObject<Something>(content)
          : null;
      }
    }
  }
}
