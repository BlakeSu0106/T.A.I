using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Identity.Application.AppServices;

namespace Telligent.Admin.Identity.Server.Controllers;

[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IdentityAppService _identityAppService;

    public AuthorizationController(IdentityAppService identityAppService)
    {
        _identityAppService = identityAppService;
    }

    [HttpPost("~/connect/token")]
    [Produces("application/json")]
    public async Task<IActionResult> ExchangeAsync()
    {
        return await _identityAppService.ExchangeAsync(HttpContext.GetOpenIddictServerRequest());
    }
}