using DataModel;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace consumer.tests
{
  public class Consumer1Tests : IClassFixture<PactMock>
  {

    private IMockProviderService _mockProviderService;
    private string _mockProviderServiceBaseUri;

    public Consumer1Tests(PactMock data)
    {
      _mockProviderService = data.MockProviderService;
      _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
      _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
    }

    [Fact]
    public void GetById_IfExists_MustReturnRecord()
    {
      //Arrange
      _mockProviderService
        .Given("There is a record of 'Something' with an id of 1")
        .UponReceiving("A GET request to retrieve 'Something'")
        .With(new ProviderServiceRequest
        {
          Method = HttpVerb.Get,
          Path = "/Something/1",
          Headers = new Dictionary<string, object>
          {
          { "Accept", "application/json" }
          }
        })
        .WillRespondWith(new ProviderServiceResponse
        {
          Status = 200,
          Headers = new Dictionary<string, object>
          {
          { "Content-Type", "application/json; charset=utf-8" }
          },
          Body = new Something
          {
            id = 1,
            textValue = "Text Value",
            dateValue = DateTime.Parse("2021-01-01T04:00:00")
          }
        }); //NOTE: WillRespondWith call must come last as it will register the interaction

      var consumer = new ApiClient(_mockProviderServiceBaseUri);

      //Act
      var response = consumer.GetSomething(1);

      //Assert
      var result = consumer.getResponseObject(response.Result);
      Assert.Equal(1, result.Result.id);

      _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once


    }
    /*
        [Fact]
        public void GetById_IfNotExists_MustReturnNot()
        {
          //Arrange
          const string givenCode = "404";
          _mockProviderService
            .Given("There is nothing with id 0")
            .UponReceiving("A GET request to retrieve the something")
            .With(new ProviderServiceRequest
            {
              Method = HttpVerb.Get,
              Path = "/somethings/0",
              Query = Match.Regex($"code={givenCode}", "code=(404)$"),
              Headers = new Dictionary<string, object>
              {
              { "Accept", "application/json" }
              }
            })
            .WillRespondWith(new ProviderServiceResponse
            {
              Status = 404
            }); //NOTE: WillRespondWith call must come last as it will register the interaction

          var consumer = new ApiClient(_mockProviderServiceBaseUri);

          //Act
          var result = consumer.GetSomething(0);

          //Assert
          Assert.Equal(HttpStatusCode.NotFound, result.Result.StatusCode);

          _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once


        }
    */
  }
}