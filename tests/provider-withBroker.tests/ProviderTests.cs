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

namespace provider_withBroker.tests
{
	public class ProviderTests : IDisposable
	{
		private readonly string _apiUri;
		private ITestOutputHelper _output;
		private readonly string _pactDirectory;
		private readonly IPactVerifier _pactVerifier;

		public ProviderTests( ITestOutputHelper output)
		{
			_output = output;
			_apiUri = "http://localhost:4010";
			_pactDirectory = "http://localhost:9292/pacts/provider/TestApi1/consumer/Consumer1/latest";

			var pactConfig = new PactVerifierConfig
			{
				PublishVerificationResults = true,
				ProviderVersion = "1.0.0",
				Outputters = new List<IOutput>
				{
					new XUnitOutputter(_output)
				},
				Verbose = true
			};
			_pactVerifier = new PactVerifier(pactConfig);
		}

		[Fact]
		public void Pact_Should_Be_Verified()
		{
			_pactVerifier
			.ServiceProvider("TestApi1", _apiUri)
			.HonoursPactWith("Consumer1")
			.PactUri(_pactDirectory)
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
