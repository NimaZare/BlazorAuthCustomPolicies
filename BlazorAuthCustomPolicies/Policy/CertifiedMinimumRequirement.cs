using Microsoft.AspNetCore.Authorization;

namespace BlazorAuthCustomPolicies.Policy
{
    public class CertifiedMinimumRequirement : IAuthorizationRequirement
    {
        public Boolean Certified { get; }
        public Int32 CertifiedNumberOfYears { get; }

        public CertifiedMinimumRequirement(Boolean certified, Int32 certifiedNumberOfYears)
        {
            Certified = certified;
            CertifiedNumberOfYears = certifiedNumberOfYears;
        }
    }
}
