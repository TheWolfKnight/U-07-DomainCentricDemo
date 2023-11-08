using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.DependencyResolver;
using System.Security.Claims;

namespace DomainCentricDemo.WebApp.Policies.Extension {
    public static class UserManagementExtension {

        public static async Task<IdentityResult> AddOrUpdateClaimAsync(this ClaimsPrincipal principal, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signIn, string claimType, string claimValue)
        {
            IdentityUser? user = await userManager.GetUserAsync(principal);

            if (user == null) return IdentityResult.Failed();

            IdentityResult result;

            Claim? oldClaim = principal.Claims.FirstOrDefault(a => a.Type == claimType);
            if (oldClaim != null) {
                result = await userManager.RemoveClaimAsync(user, oldClaim);
                if (result != IdentityResult.Success) return result;

                principal.Identities.FirstOrDefault()?.RemoveClaim(oldClaim);
            }

            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);

            result = await userManager.AddClaimAsync(user, claim);
            if (result != IdentityResult.Success) return result;

            principal.Identities.FirstOrDefault()?.AddClaim(claim);
            if (result.Succeeded) await signIn.SignInAsync(user, false);

            return result;
        }

        public static async Task<IdentityResult> DeleteClaimAsync(this ClaimsPrincipal principal, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signIn, string claimType)
        {
            IdentityUser? user = await userManager.GetUserAsync(principal);

            if (user == null) return IdentityResult.Failed();

            IdentityResult result;

            Claim? oldClaim = principal.Claims.FirstOrDefault(a => a.Type == claimType);
            if (oldClaim == null) throw new ArgumentException("Claim not found");

            result = await userManager.RemoveClaimAsync(user, oldClaim);
            if (result != IdentityResult.Success) return result;

            principal.Identities.FirstOrDefault()?.RemoveClaim(oldClaim);
            await signIn.SignInAsync(user, false);

            return result;
        }

    }
}
