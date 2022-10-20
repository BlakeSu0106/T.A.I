using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Application.AppServices;
using Telligent.Admin.Identity.Application.Dtos.AdminUser;

namespace Telligent.Admin.Identity.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminUserController : ControllerBase
{
    private readonly AdminUserAppService _service;

    public AdminUserController(AdminUserAppService service)
    {
        _service = service;
    }

    /// <summary>
    /// 建立網訊管理者帳號
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateAdminUserDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}
