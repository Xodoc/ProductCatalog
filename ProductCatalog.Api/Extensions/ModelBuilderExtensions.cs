using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Entities;

namespace ProductCatalog.Api.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static readonly string _password = "123As!";

        private static string[]? _roleIds;
        private static string[]? _userIds;

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            return builder.FillCatigories()
                          .FillProducts()
                          .FillRoles()
                          .FillUsers()
                          .FillUserRoles();
        }

        public static ModelBuilder FillRoles(this ModelBuilder builder)
        {
            var roles = new IdentityRole[]
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Advanced",
                    NormalizedName = "Advanced".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            };

            _roleIds = new string[roles.Length];

            for (int i = 0; i < roles.Length; i++)
            {
                _roleIds[i] = roles[i].Id;
            }

            builder.Entity<IdentityRole>().HasData(roles);

            return builder;
        }

        public static ModelBuilder FillUsers(this ModelBuilder builder)
        {
            var name1 = "Peter";
            var email1 = "admin@gmail.com";
            var name2 = "Lily";
            var email2 = "advanced@gmail.com";
            var name3 = "Ivan";
            var email3 = "user@gmail.com";

            var passwordHasher = new PasswordHasher<User>();

            var users = new User[]
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = name1,
                    NormalizedUserName = name1.ToUpper(),
                    Email = email1,
                    NormalizedEmail = email1.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = passwordHasher.HashPassword(null, _password),
                    PhoneNumber = "+123656787",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    AccessFailedCount = 0
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = name2,
                    NormalizedUserName = name2.ToUpper(),
                    Email = email2,
                    NormalizedEmail = email2.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = passwordHasher.HashPassword(null, _password),
                    PhoneNumber = "+125656787",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    AccessFailedCount = 0
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = name3,
                    NormalizedUserName = name3.ToUpper(),
                    Email = email3,
                    NormalizedEmail = email3.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = passwordHasher.HashPassword(null, _password),
                    PhoneNumber = "+325656787",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    AccessFailedCount = 0
                }
            };

            _userIds = new string[users.Length];

            for (int i = 0; i < users.Length; i++)
            {
                _userIds[i] = users[i].Id;
            }

            builder.Entity<User>().HasData(users);

            return builder;
        }

        public static ModelBuilder FillUserRoles(this ModelBuilder builder)
        {
            var userRoles = new List<IdentityUserRole<string>>();

            for (int i = 0; i < _roleIds!.Length; i++)
            {
                userRoles.Add(new IdentityUserRole<string>
                {
                    UserId = _userIds![i],
                    RoleId = _roleIds![i],
                });
            }

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            return builder;
        }

        public static ModelBuilder FillCatigories(this ModelBuilder builder)
        {
            var categories = new Category[]
            {
                new()
                {
                    Id = 1,
                    Name = "Еда"
                },
                new()
                {
                    Id = 2,
                    Name = "Вкусности"
                },
                new()
                {
                    Id = 3,
                    Name = "Вода"
                },
            };

            builder.Entity<Category>().HasData(categories);

            return builder;
        }

        public static ModelBuilder FillProducts(this ModelBuilder builder)
        {
            var products = new Product[]
            {
                new()
                {
                    Id = 1,
                    Name = "Селедка",
                    CategoryId = 1,
                    Description = "Селедка соленая",
                    CostInRubles = 10000,
                    GeneralNote = "Акция",
                    SpecialNote = "Пересоленная"
                },
                new()
                {
                    Id = 2,
                    Name = "Тушенка",
                    CategoryId = 1,
                    Description = "Тушенка говяжья",
                    CostInRubles = 20000,
                    GeneralNote = "Вкусная",
                    SpecialNote = "Жилы"
                },
                new()
                {
                    Id = 3,
                    Name = "Сгущенка",
                    CategoryId = 2,
                    Description = "В банках",
                    CostInRubles = 30000,
                    GeneralNote = "С ключом",
                    SpecialNote = "Вкусная"
                },
                new()
                {
                    Id = 4,
                    Name = "Квас",
                    CategoryId = 3,
                    Description = "В бутылках",
                    CostInRubles = 15000,
                    GeneralNote = "Вятский",
                    SpecialNote = "Теплый"
                },
            };

            builder.Entity<Product>().HasData(products);

            return builder;
        }
    }
}
