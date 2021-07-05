using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace provider.tests
{
	class XUnitOutputter : IOutput
	{
		private readonly ITestOutputHelper _output;

		public XUnitOutputter(ITestOutputHelper output)
		{
			_output = output;
		}

		public void WriteLine(string line)
		{
			_output.WriteLine(line);
		}
	}
}
