namespace Iels.Rbac.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string ObjectId { get; set; } = null!; // Entra OID / sub
    public string Name { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

