using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Iels.Rbac.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Iels.Rbac.Core.Services;

public class CurrentUserContext<TDbContext> : ICurrentUserContext
    where TDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TDbContext _db;
    private bool _loaded;
    private Guid? _userEntityId;
    private HashSet<string> _permissions = new(StringComparer.OrdinalIgnoreCase);

    public CurrentUserContext(IHttpContextAccessor accessor, TDbContext db)
    {
        _httpContextAccessor = accessor;
        _db = db;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string? UserId =>
        User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

    public string? UserName => User?.FindFirst("name")?.Value ?? User?.Identity?.Name;

    public IReadOnlyCollection<string> Roles =>
        User?.FindAll("roles").Select(c => c.Value).ToArray()
        ?? Array.Empty<string>();

    public bool HasRole(string role) => Roles.Contains(role);
    public bool HasAnyRole(params string[] roles) => Roles.Any(r => roles.Contains(r));

    private void EnsureLoaded()
    {
        if (_loaded || UserId is null) return;

        var users = _db.Set<User>();
        var userRoles = _db.Set<UserRole>();
        var rolePermissions = _db.Set<RolePermission>();
        var permissions = _db.Set<Permission>();

        var user = users.SingleOrDefault(u => u.ObjectId == UserId);
        if (user is null)
        {
            _loaded = true;
            return;
        }

        _userEntityId = user.Id;

        _permissions = userRoles
            .Where(ur => ur.UserId == user.Id)
            .SelectMany(ur => rolePermissions
                .Where(rp => rp.RoleId == ur.RoleId)
                .Join(permissions,
                    rp => rp.PermissionId,
                    p => p.Id,
                    (_, p) => p.Name))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        _loaded = true;
    }

    public bool HasPermission(string permission)
    {
        EnsureLoaded();
        return _permissions.Contains(permission);
    }
}
