using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsersAPI.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinimumAge>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAge requirement)
    {
        var birthDateClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (birthDateClaim is null) return Task.CompletedTask;

        var birthDate = Convert.ToDateTime(birthDateClaim.Value);

        var userAge = DateTime.UtcNow.Year - birthDate.Year;

        if (birthDate > DateTime.UtcNow.AddYears(-userAge)) userAge--;

        if (userAge >= requirement.Age) 
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
