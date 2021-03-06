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
		private readonly string _apiUri;
		private ITestOutputHelper _output;
		private readonly string _pactDirectory;
		private IPactVerifier _pactVerifier;


		public ProviderTests( ITestOutputHelper output)
		{
			_output = output;
			_apiUri = "http://localhost:4010";
			_pactDirectory = @"..\..\..\..\..\pacts\consumer1-testapi1.json";

			var pactConfig = new PactVerifierConfig
			{
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
