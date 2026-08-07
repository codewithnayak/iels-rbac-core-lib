namespace Iels.Rbac.Core.Entities;

public class Permission
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; // FIR.Create, Station.Manage, etc.

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}