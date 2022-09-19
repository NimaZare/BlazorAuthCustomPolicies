using Microsoft.AspNetCore.Authorization;

namespace BlazorAuthCustomPolicies.Policy
{
    public class CertifiedMinimumHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            foreach (var requirement in pendingRequirements)
            {
                if (requirement is CertifiedMinimumRequirement)
                {
                    var certifiedMinimumRequirement = requirement as CertifiedMinimumRequirement;
                    var certified = context.User.FindFirst(c => c.Type == "Certified");
                    var certifiedNumberOfYears = context.User.FindFirst(c => c.Type == "CertifiedNumberOfYears");
                    Boolean isCertified = false;
                    Boolean.TryParse(certified?.Value, out isCertified);
                    Int32 yearsCertified = 0;
                    Int32.TryParse(certifiedNumberOfYears?.Value, out yearsCertified);

                    if (isCertified && yearsCertified >= 5)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
                else if (requirement is MinimumAgeRequirement)
                {
                    var minAgeRequirement = requirement as MinimumAgeRequirement;
                    var dateOfBirthClaim = context.User.FindFirst(c => c.Type == "Age");
                    if (dateOfBirthClaim is null)
                    {
                        context.Fail();
                    }

                    var calculatedAge = Int32.Parse(dateOfBirthClaim?.Value ?? "0");
                    calculatedAge *= 7;
                    if (calculatedAge >= minAgeRequirement?.MinimumAge)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
