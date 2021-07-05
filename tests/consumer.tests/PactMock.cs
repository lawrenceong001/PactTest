using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks.MockHttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer.tests
{
	public class PactMock : IDisposable
	{
		private readonly IPactBuilder _pactBuilder;
		public int MockServerPort { get { return 9222; } }
		public IMockProviderService MockProviderService { get; }

		public string MockProviderServiceBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }



		public PactMock()
		{
			var pactConfig = new PactConfig
			{
				SpecificationVersion = "2.0.0",
				PactDir = @"..\..\..\..\..\pacts",
				LogDir = @"..\..\..\..\..\..\logs"
			};

			_pactBuilder = new PactBuilder(pactConfig)
			.ServiceConsumer("Consumer1")
			.HasPactWith("TestApi1");

			MockProviderService = _pactBuilder.MockService(MockServerPort, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
		}

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_pactBuilder.Build();
				}
				_disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}

	}
}
