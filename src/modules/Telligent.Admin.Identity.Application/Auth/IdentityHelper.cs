using System.Security.Claims;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Telligent.Admin.Identity.Application.Auth
{
    public static class IdentityHelper
    {
        public static IEnumerable<string> GetDestinations(Claim claim)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                case Claims.Subject:
                    yield return Destinations.AccessToken;

                    if (claim.Subject != null && claim.Subject.HasScope(Permissions.Scopes.Profile))
                        yield return Destinations.IdentityToken;

                    yield break;
                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (claim.Subject != null && claim.Subject.HasScope(Permissions.Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (claim.Subject != null && claim.Subject.HasScope(Permissions.Scopes.Roles))
                        yield return Destinations.IdentityToken;

                    yield break;

                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }
    }
}
