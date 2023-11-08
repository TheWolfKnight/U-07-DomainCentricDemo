using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.WebApp.Policies.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace DomainCentricDemo.WebApp.Policies.Handler {
    public class IsSoleAuthorOrAdminHandler : AuthorizationHandler<IsSoleAuthorOrAdminRequirement, BookDto> {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsSoleAuthorOrAdminRequirement requirement, BookDto resource) {
            if (!context.User.HasClaim(c => c.Type == ClaimsType.Admin || c.Type == ClaimsType.Author)) {
                context.Fail();
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(c => c.Type == ClaimsType.Admin)) {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (resource.Authors!.Count() > 1 ||
                resource.Authors!.FirstOrDefault()?.Id.ToString() != context.User.Identity?.Name)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

    }
}
