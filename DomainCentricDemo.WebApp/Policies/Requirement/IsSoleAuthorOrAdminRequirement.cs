using Microsoft.AspNetCore.Authorization;

namespace DomainCentricDemo.WebApp.Policies.Requirement {
    public class IsSoleAuthorOrAdminRequirement : IAuthorizationRequirement {
    }
}
