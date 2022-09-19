using Microsoft.AspNetCore.Authorization;

namespace BlazorAuthCustomPolicies.Policy
{
    public class AdminClaimRequirement : IAuthorizationRequirement
    {
        public Boolean IsAdmin { get; }

        public AdminClaimRequirement(Boolean isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
