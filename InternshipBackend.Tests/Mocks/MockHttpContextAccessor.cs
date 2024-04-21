using Microsoft.AspNetCore.Http;

namespace InternshipBackend.Tests.Mocks;

public class MockHttpContextAccessor : IHttpContextAccessor
{
    public HttpContext? HttpContext { get; set; }
}