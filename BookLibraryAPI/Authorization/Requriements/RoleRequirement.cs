using Microsoft.AspNetCore.Authorization;

namespace BookLibraryAPI.Authorization.Requriements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public ICollection<string> Roles { get; set; }

        public RoleRequirement(ICollection<string> roles)
        {
            Roles = roles;
        }
    }
}
