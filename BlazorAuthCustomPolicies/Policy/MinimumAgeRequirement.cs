using Microsoft.AspNetCore.Authorization;

namespace BlazorAuthCustomPolicies.Policy
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public Int32 MinimumAge { get; }
        public MinimumAgeRequirement(Int32 minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
