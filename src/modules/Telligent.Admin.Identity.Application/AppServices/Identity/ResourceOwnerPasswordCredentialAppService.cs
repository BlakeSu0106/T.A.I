using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Telligent.Admin.Identity.Application.Auth;
using Telligent.Admin.Identity.Domain.Users;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Services;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Telligent.Admin.Identity.Application.AppServices.Identity;

public class ResourceOwnerPasswordCredentialAppService : ControllerBase, IAppService
{
    private readonly IRepository<AdminUser> _adminUserRepository;
    private readonly IConfiguration _configuration;

    public ResourceOwnerPasswordCredentialAppService(
        IRepository<AdminUser> adminUserRepository,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _adminUserRepository = adminUserRepository;
    }

    /// <summary>
    /// resource owner password credential flow authentication
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IActionResult> ExchangeAsync(OpenIddictRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                    "The username/password couple is invalid."
            });

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var salt = _configuration.GetSection("Salt").Value;

        var encryptPassword = EncryptionHelper.EncryptArgon2(request.Password, System.Text.Encoding.Default.GetBytes(salt));

        var admin = await _adminUserRepository.GetAsync(
            user => user.UserId.Equals(request.Username) &&
                    user.Password.Equals(encryptPassword) &&
                    user.EntityStatus);

        if (admin == null)
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                    "The username/password couple is invalid."
            });

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)
            .AddClaim(Claims.Subject, admin.Id.ToString()) // id
            .AddClaim(Claims.Email, admin.Email)
            .AddClaim(Claims.Name, admin.Name);

        // Set the list of scopes granted to the client application.
        identity.SetScopes(new[]
        {
            Scopes.OpenId,
            Scopes.Email,
            Scopes.Profile,
            Scopes.Roles
        }.Intersect(request.GetScopes()));

        identity.SetDestinations(IdentityHelper.GetDestinations);

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}