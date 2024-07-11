using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Domain.Enum;
using System.Collections.Generic;

namespace ShortenUrl.Persistence.Seeds
{
    public static class ContextSeed
    {
        public static void SeedUsersAndRoles(this ModelBuilder modelBuilder)
        {
            CreateRoles(modelBuilder);
            CreateUsers(modelBuilder);
            AssignRolesToUsers(modelBuilder);
        }

        private static void CreateRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = Roles.Admin.ToString(),
                    NormalizedName = Roles.Admin.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = Roles.BasicUser.ToString(),
                    NormalizedName = Roles.BasicUser.ToString().ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        private static void CreateUsers(ModelBuilder modelBuilder)
        {
            var users = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "1",
                    UserName = "john.doe@example.com",
                    NormalizedUserName = "JOHN.DOE@EXAMPLE.COM",
                    Email = "john.doe@example.com",
                    NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!")
                },
                new IdentityUser
                {
                    Id = "2",
                    UserName = "jane.smith@example.com",
                    NormalizedUserName = "JANE.SMITH@EXAMPLE.COM",
                    Email = "jane.smith@example.com",
                    NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!")
                },
                new IdentityUser
                {
                    Id = "3",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
                }
            };

            modelBuilder.Entity<IdentityUser>().HasData(users);
        }

        private static void AssignRolesToUsers(ModelBuilder modelBuilder)
        {
            var userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "2" 
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "3",
                    RoleId = "1" 
                }
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
