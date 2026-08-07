using Iels.Rbac.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iels.Rbac.Core.Extensions;

public static class ModelBuilderExtensions
{
    public static void AddIelsRbacModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });
    }
}