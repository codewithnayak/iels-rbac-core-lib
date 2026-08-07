using Microsoft.AspNetCore.Authorization;

namespace Iels.Rbac.Core.Authorization;

public class IelsRequirement : IAuthorizationRequirement
{
    public string? Permission { get; }

    public IelsRequirement(string? permission = null)
    {
        Permission = permission;
    }
}