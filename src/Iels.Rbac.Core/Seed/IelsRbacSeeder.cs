using System;
using Iels.Rbac.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iels.Rbac.Core.Seed;

public static class IelsRbacSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var dgUserId  = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var spUserId  = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var shoUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var ioUserId  = Guid.Parse("44444444-4444-4444-4444-444444444444");

        var dgRoleId  = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1");
        var spRoleId  = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2");
        var shoRoleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3");
        var offRoleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4");

        var pViewFir      = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb001");
        var pCreateFir    = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb002");
        var pEditFir      = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb003");
        var pCloseFir     = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb004");
        var pApproveFir   = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb005");
        var pAssignOfficer= Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb006");

        modelBuilder.Entity<User>().HasData(
            new User { Id = dgUserId,  ObjectId = "dg-001",  Name = "DG Police" },
            new User { Id = spUserId,  ObjectId = "sp-001",  Name = "SP Pune" },
            new User { Id = shoUserId, ObjectId = "sho-001", Name = "SHO Shivajinagar" },
            new User { Id = ioUserId,  ObjectId = "io-001",  Name = "Investigating Officer" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = dgRoleId,  Name = "DG" },
            new Role { Id = spRoleId,  Name = "SP" },
            new Role { Id = shoRoleId, Name = "SHO" },
            new Role { Id = offRoleId, Name = "Officer" }
        );

        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = pViewFir,       Name = "FIR.View" },
            new Permission { Id = pCreateFir,     Name = "FIR.Create" },
            new Permission { Id = pEditFir,       Name = "FIR.Edit" },
            new Permission { Id = pCloseFir,      Name = "FIR.Close" },
            new Permission { Id = pApproveFir,    Name = "FIR.Approve" },
            new Permission { Id = pAssignOfficer, Name = "FIR.AssignOfficer" }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = dgUserId,  RoleId = dgRoleId },
            new UserRole { UserId = spUserId,  RoleId = spRoleId },
            new UserRole { UserId = shoUserId, RoleId = shoRoleId },
            new UserRole { UserId = ioUserId,  RoleId = offRoleId }
        );

        modelBuilder.Entity<RolePermission>().HasData(
            new RolePermission { RoleId = dgRoleId, PermissionId = pViewFir },
            new RolePermission { RoleId = dgRoleId, PermissionId = pCreateFir },
            new RolePermission { RoleId = dgRoleId, PermissionId = pEditFir },
            new RolePermission { RoleId = dgRoleId, PermissionId = pCloseFir },
            new RolePermission { RoleId = dgRoleId, PermissionId = pApproveFir },
            new RolePermission { RoleId = dgRoleId, PermissionId = pAssignOfficer },

            new RolePermission { RoleId = spRoleId, PermissionId = pViewFir },
            new RolePermission { RoleId = spRoleId, PermissionId = pCreateFir },
            new RolePermission { RoleId = spRoleId, PermissionId = pEditFir },
            new RolePermission { RoleId = spRoleId, PermissionId = pCloseFir },
            new RolePermission { RoleId = spRoleId, PermissionId = pApproveFir },

            new RolePermission { RoleId = shoRoleId, PermissionId = pViewFir },
            new RolePermission { RoleId = shoRoleId, PermissionId = pCreateFir },
            new RolePermission { RoleId = shoRoleId, PermissionId = pEditFir },

            new RolePermission { RoleId = offRoleId, PermissionId = pViewFir },
            new RolePermission { RoleId = offRoleId, PermissionId = pEditFir }
        );
    }
}
