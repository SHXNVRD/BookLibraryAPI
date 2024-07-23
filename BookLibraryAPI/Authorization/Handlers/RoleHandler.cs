using BookLibraryAPI.Authorization.Requriements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BookLibraryAPI.Authorization.Handlers
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);

            if (role == null) 
                return Task.CompletedTask;

            string[] roles = [role.Value];

            if (roles.Intersect(requirement.Roles).Any())
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
