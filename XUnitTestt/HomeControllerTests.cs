using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestaoVigilanciaGP2018.Models;
using SistemaGestaoVigilanciaGP2018.Tests;
using Xunit;
using SistemaGestaoVigilanciaGP2018.Data;
using SistemaGestaoVigilanciaGP2018.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SistemaGestaoVigilanciaGP2018.Tests
{
    public class HomeControllerUnitTests
    {
        private readonly IServiceProvider _serviceProvider;

        public HomeControllerUnitTests()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("Scratch").UseInternalServiceProvider(efServiceProvider));

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task TestIndex()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new HomeController();

            // Acção
            var result = controller.Index();

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task TestAbout()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new HomeController();

            // Acção
            var result = controller.About();

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task TestContact()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new HomeController();

            // Acção
            var result = controller.Contact();

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task TestError()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestUser(dbContext: dbContext);
            var controller = new HomeController();

            // Acção
            var userId = dbContext.Users.AsNoTracking().Where(u => u.Id == "A").FirstOrDefault();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userId.Id)
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.Error();

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        private static ApplicationUser[] CreateTestUser(DbContext dbContext)
        {
            ApplicationUser[] user = new ApplicationUser[3];
            ApplicationUser user1 = new ApplicationUser();

            user1.AccessFailedCount = 0;
            user1.NumeroVigias = 0;
            user1.ConcurrencyStamp = "ND";
            user1.Email = "tiagod@gmail.com";
            user1.EmailConfirmed = true;
            user1.Id = "A";
            user1.LockoutEnabled = false;
            user1.LockoutEnd = null;
            user1.Nacionalidade = "Portugal";
            user1.NormalizedEmail = "TIAGOD@GMAIL.COM";
            user1.NormalizedUserName = "TIAGO DIAS";
            user1.NumeroDocente = "17";
            user1.PasswordHash = null;
            user1.PhoneNumber = "912345678";
            user1.PhoneNumberConfirmed = false;
            user1.DataNascimento = new DateTime(1998, 04, 30);
            user1.RoleType = "Docente";
            user1.SecurityStamp = null;
            user1.TwoFactorEnabled = false;
            user1.UserName = "Tiago Dias";

            user[0] = user1;
            dbContext.Add(user1);

            ApplicationUser user2 = new ApplicationUser();

            user2.AccessFailedCount = 0;
            user2.NumeroVigias = 0;
            user1.ConcurrencyStamp = "ND";
            user2.Email = "diogogomes@gmail.com";
            user2.EmailConfirmed = true;
            user2.Id = "B";
            user2.LockoutEnabled = false;
            user2.LockoutEnd = null;
            user2.Nacionalidade = "Portugal";
            user2.NormalizedEmail = "DIOGOG@GMAIL.COM";
            user2.NormalizedUserName = "DIOGO GOMES";
            user2.NumeroDocente = "1765";
            user2.PasswordHash = null;
            user2.PhoneNumber = "985954678";
            user2.PhoneNumberConfirmed = false;
            user2.DataNascimento = new DateTime(1992, 04, 05);
            user2.RoleType = "Docente";
            user2.SecurityStamp = null;
            user2.TwoFactorEnabled = false;
            user2.UserName = "Diogo Gomes";

            user[1] = user2;
            dbContext.Add(user2);

            ApplicationUser user3 = new ApplicationUser();

            user3.AccessFailedCount = 0;
            user3.NumeroVigias = 0;
            user3.ConcurrencyStamp = "ND";
            user3.Email = "patricia@gmail.com";
            user3.EmailConfirmed = true;
            user3.Id = "C";
            user3.LockoutEnabled = false;
            user3.LockoutEnd = null;
            user3.Nacionalidade = "Portugal";
            user3.NormalizedEmail = "PATI@GMAIL.COM";
            user3.NormalizedUserName = "PATRICIA GOMES";
            user3.NumeroDocente = "18";
            user3.PasswordHash = null;
            user3.PhoneNumber = "985347978";
            user3.PhoneNumberConfirmed = false;
            user3.DataNascimento = new DateTime(1989, 09, 30);
            user3.RoleType = "Docente";
            user3.SecurityStamp = null;
            user3.TwoFactorEnabled = false;
            user3.UserName = "Patricia Gomes";

            user[2] = user3;
            dbContext.Add(user3);

            dbContext.SaveChanges();

            return user;
        }

    }
    }
