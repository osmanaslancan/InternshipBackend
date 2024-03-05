using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Core.Services;

[Authorize]
public abstract class BaseEndpoint : Controller
{
}
