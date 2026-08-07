using System.Threading.Tasks;
using Iels.Rbac.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace Iels.Rbac.Core.Authorization;

public class IelsAuthorizationHandler : AuthorizationHandler<IelsRequirement>
{
    private readonly ICurrentUserContext _currentUser;

    public IelsAuthorizationHandler(ICurrentUserContext currentUser)
    {
        _currentUser = currentUser;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        IelsRequirement requirement)
    {
        if (string.IsNullOrEmpty(_currentUser.UserId))
            return Task.CompletedTask;

        if (!_currentUser.HasAnyRole("DG", "SP", "SHO", "Officer"))
            return Task.CompletedTask;

        if (!string.IsNullOrEmpty(requirement.Permission) &&
            !_currentUser.HasPermission(requirement.Permission))
            return Task.CompletedTask;

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}