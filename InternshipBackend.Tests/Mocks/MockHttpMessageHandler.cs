using System.Net;
using Microsoft.Extensions.Http;

namespace InternshipBackend.Tests.Mocks;

public class MockHttpMessageHandlerBuilder : HttpMessageHandlerBuilder
{
    public override HttpMessageHandler Build()
    {
        throw new NotImplementedException();
    }

    public override IList<DelegatingHandler> AdditionalHandlers { get; }
    public override string? Name { get; set; }
    public override HttpMessageHandler PrimaryHandler { get; set; }
}

public class MockHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{'id': 1, 'name': 'Test'}")
        });
    }
}