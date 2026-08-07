using System;
using System.Collections.Generic;

namespace Iels.Rbac.Core.Services;

public interface ICurrentUserContext
{
    string? UserId { get; }
    string? UserName { get; }
    IReadOnlyCollection<string> Roles { get; }

    bool HasRole(string role);
    bool HasAnyRole(params string[] roles);

    bool HasPermission(string permission);
}