using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestaoVigilanciaGP2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoVigilanciaGP2018.Data
{
    public static class RolesData
    {
        private static readonly string[] Roles = new string[]
        {
            "Administrador","Utilizador"
        };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByNameAsync("admin@grupo2.gp");
                if (user == null)
                {
                    var admin = new ApplicationUser()
                    {
                        UserName = "admin@grupo2.gp",
                        PrimeiroNome = "Admin",
                        UltimoNome = "Admin",
                        Email = "admin@grupo2.gp",
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    await userManager.CreateAsync(admin, "AdminGrupo2");
                    admin.EmailConfirmed = true;

                    var appUser = await userManager.FindByNameAsync("admin@grupo2.gp");
                    await userManager.AddToRoleAsync(appUser, "Administrador");
                }

                user = await userManager.FindByNameAsync("user@grupo2.gp");
                if (user == null)
                {
                    var testUser = new ApplicationUser()
                    {
                        UserName = "user@grupo2.gp",
                        PrimeiroNome = "Gestao",
                        UltimoNome = "Projetos",
                        Email = "user@grupo2.gp",
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    await userManager.CreateAsync(testUser, "UserGrupo2");
                    testUser.EmailConfirmed = true;

                    var appUser = await userManager.FindByNameAsync("user@grupo2.gp");
                    await userManager.AddToRoleAsync(appUser, "Utilizador");
                }
            }
        }
    }
}
