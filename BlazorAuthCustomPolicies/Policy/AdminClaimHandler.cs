using Microsoft.AspNetCore.Authorization;

namespace BlazorAuthCustomPolicies.Policy
{
    public class AdminClaimHandler : AuthorizationHandler<AdminClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminClaimRequirement requirement)
        {
            var isAdminUser = context.User.FindFirst(c => c.Type == "AdminPolicy");
            if (isAdminUser is null)
            {
                return Task.CompletedTask;
            }

            Boolean isCertified = false;
            Boolean.TryParse(isAdminUser?.Value, out isCertified);

            if (isCertified)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
