using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Mocks.MockHttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace provider.tests
{
	public class ProviderTests : IDisposable
	{
		private readonly IPactBuilder _pactBuilder;
		public int MockServerPort { get { return 9222; } }
		public IMockProviderService MockProviderService { get; }

		public string MockProviderServiceBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }

		private readonly string _apiUri;
		private ITestOutputHelper _output;
		//private readonly IWebHost _webHost;

		public ProviderTests( ITestOutputHelper output)
		{
			_output = output;
			_apiUri = "http://localhost:4010";
			//_webHost = WebHost.CreateDefaultBuilder();


		}

		[Fact]
		public void Pact_Should_Be_Verified()
		{
			var pactConfig = new PactVerifierConfig
			{
				Outputters = new List<IOutput> 
				{ 
				new XUnitOutputter(_output)
				},
				Verbose = true
			};
			new PactVerifier(pactConfig)
			.ServiceProvider("TestApi1", _apiUri)
			.HonoursPactWith("Consumer1")
			.PactUri(@"..\..\..\..\..\pacts\consumer1-testapi1.json")
			.Verify();

		}

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					//
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
