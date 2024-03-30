using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.App;

[Route("Enum")]
public class EnumController(IEnumService enumService) : BaseEndpoint
{
    [HttpGet("Get"), AllowAnonymous]
    public ActionResult<List<EnumDto>> GetEnum([FromQuery] string key)
    {
        var result = enumService.GetEnumOrDefault(key);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
