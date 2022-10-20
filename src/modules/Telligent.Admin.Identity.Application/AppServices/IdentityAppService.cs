using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using Telligent.Admin.Identity.Application.AppServices.Identity;
using Telligent.Core.Infrastructure.Services;

namespace Telligent.Admin.Identity.Application.AppServices;

public class IdentityAppService : IAppService
{
    private readonly ResourceOwnerPasswordCredentialAppService _ropcAppService;

    public IdentityAppService(
        ResourceOwnerPasswordCredentialAppService ropcAppService)
    {
        _ropcAppService = ropcAppService;
    }

    public async Task<IActionResult> ExchangeAsync(OpenIddictRequest request)
    {
        if (request == null)
            throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (request.IsPasswordGrantType())
            return await _ropcAppService.ExchangeAsync(request);

        throw new InvalidOperationException("The specified grant type is not supported.");
    }
}