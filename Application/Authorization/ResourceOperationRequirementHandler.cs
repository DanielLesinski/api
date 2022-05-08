using Domain.Entities.Announcements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Announcement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ResourceOperationRequirement requirement, Announcement announcement)
        {
            if(requirement.ResorceOperation == ResorceOperation.Read ||
                requirement.ResorceOperation == ResorceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if(announcement.UserId == int.Parse(userId) || role == "Admin" || role == "Moderator")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
