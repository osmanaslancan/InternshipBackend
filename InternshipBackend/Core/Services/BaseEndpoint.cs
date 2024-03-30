using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Core.Services;

[Authorize]
[ApiController]
[Produces("application/json")]
public abstract class BaseEndpoint : Controller
{
}
