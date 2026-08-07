namespace Iels.Rbac.Core.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; // DG, SP, SHO, Officer

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}