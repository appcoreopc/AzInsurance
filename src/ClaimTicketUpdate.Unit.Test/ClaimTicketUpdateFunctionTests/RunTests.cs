using System;
using Xunit;
using Microsoft.Extensions.Logging;
using FakeItEasy;
using Microsoft.Azure.WebJobs;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ClaimTicketUpdate.Unit.Test.ClaimTicketUpdateFunctionTests
{
    public class RunTests
    {
        private ILogger logger;
        private ExecutionContext ctx;
        private HttpRequest httpMessage;

        [Fact]
        public void WhenTriggeredThenActivityGetsCalled()
        {
            this.SetupFakes();
            var target = ClaimTicketUpdateFunction.Run(this.httpMessage, this.logger, this.ctx);
        }

        private void SetupFakes()
        {
            this.logger = A.Fake<ILogger>();
            this.ctx = new ExecutionContext();
            this.httpMessage = A.Fake<HttpRequest>();
}
    }
}
