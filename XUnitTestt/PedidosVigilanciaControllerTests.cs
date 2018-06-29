using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using RestSharp;
using RestSharp.Authenticators;
using SistemaGestaoVigilanciaGP2018.Data;
using SistemaGestaoVigilanciaGP2018.Models;
using SistemaGestaoVigilanciaGP2018.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Hosting;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using SistemaGestaoVigilanciaGP2018.Controllers;
using Microsoft.AspNetCore.Http;

namespace SistemaGestaoVigilanciaGP2018.Tests
{
    public class PedidosVigilanciaControllerTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;


        public PedidosVigilanciaControllerTests()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();
            //_hostingEnvironment = hostingEnvironment;

            services.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("Scratch").UseInternalServiceProvider(efServiceProvider));

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task TestIndex()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new PedidoVigilanciasController(dbContext, _emailSender);

            // Acção
            var result = await controller.Index();

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task TestDetails()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestPedidos(dbContext: dbContext);
            var controller = new PedidoVigilanciasController(dbContext, _emailSender);

            // Acção
            var result = await controller.Details(1);

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task TestCreate()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new PedidoVigilanciasController(dbContext, _emailSender);
            // Verificação

            PedidoVigilancia pedido = CreateTestPedidos(dbContext)[1];
            pedido.NumeroDocente = "1";
            var result = await controller.Create(pedido);

            // Avaliação
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            var viewResultName = viewResult.ActionName;
            Assert.Equal("Index", viewResultName);
        }

        [Fact]
        public async Task TestEdit()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestUser(dbContext: dbContext);
            var controller = new PedidoVigilanciasController(dbContext, _emailSender);

            // Acção
            PedidoVigilancia pedi3 = CreateTestPedidos(dbContext: dbContext)[2];

            var userId = dbContext.Users.AsNoTracking().Where(u => u.Id == "A").FirstOrDefault();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userId.Id)
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = await controller.Edit(1);
            var result2 = await controller.Edit(3, pedi3);

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var editResult = Assert.IsType<RedirectToActionResult>(result2);
            var editResultName = editResult.ActionName;
            Assert.Equal("Index", editResultName);
        }

        [Fact]
        public async Task TestDelete()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestPedidos(dbContext: dbContext);
            var controller = new PedidoVigilanciasController(dbContext, _emailSender);

            // Acção
            var result = await controller.Delete(1);
            var result2 = await controller.DeleteConfirmed(1);

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var deleteResult = Assert.IsType<RedirectToActionResult>(result2);
            var deleteResultName = deleteResult.ActionName;
            Assert.Equal("Index", deleteResultName);
        }

        [Fact]
        public async Task TestCriarPedido()
        {
            // Instanciação
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestUser(dbContext: dbContext);
            var controller = new PedidoVigilanciasController(dbContext, _emailSender);

            // Acção
            PedidoVigilancia pedi3 = new PedidoVigilancia();

            pedi3.IdPedido = 125;
            pedi3.NumeroDocente = "2010";
            pedi3.PrimeiroNome = "Diogo";
            pedi3.UltimoNome = "Rodrigues";
            pedi3.CursoId = 1;
            pedi3.UCid = 1;
            pedi3.Sala = "D205";
            pedi3.DataVigilancia = new DateTime(2018, 10, 05);
            pedi3.HoraVigilancia = new DateTime(2018, 10, 05, 12, 30, 00);
            pedi3.ConfirmarVigia = true;
            pedi3.Motivo = "";
            pedi3.TipoEstado = Estado.EmEspera;

            var userId = dbContext.Users.AsNoTracking().Where(u => u.Id == "A").FirstOrDefault();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userId.Id)
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.FazerPedido(pedi3);
            var result2 = await controller.FazerPedido(pedi3);

            // Avaliação
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var cand1Result = Assert.IsType<RedirectToActionResult>(result2);
            var cand1ResultName = cand1Result.ActionName;
            Assert.Equal("Index", cand1ResultName);
        }

        private static PedidoVigilancia[] CreateTestPedidos(DbContext dbContext)
        {
            PedidoVigilancia[] pedido = new PedidoVigilancia[3];
            PedidoVigilancia pedi1 = new PedidoVigilancia();

            pedi1.IdPedido = 56;
            pedi1.NumeroDocente = "2010";
            pedi1.PrimeiroNome = "Tiago";
            pedi1.UltimoNome = "Dias";
            pedi1.CursoId = 1;
            pedi1.UCid = 1;
            pedi1.Sala = "E105";
            pedi1.DataVigilancia = new DateTime(2018,06,20);
            pedi1.HoraVigilancia = new DateTime(2018, 06, 20,12,30,00);
            pedi1.ConfirmarVigia = false;
            pedi1.Motivo = "Estou doente";
            pedi1.TipoEstado = Estado.recusado;

            pedido[0] = pedi1;
            dbContext.Add(pedi1);

            PedidoVigilancia pedi2 = new PedidoVigilancia();

            pedi2.IdPedido = 63;
            pedi2.NumeroDocente = "852";
            pedi2.PrimeiroNome = "Diogo";
            pedi2.UltimoNome = "Gomes";
            pedi2.CursoId = 1;
            pedi2.UCid = 1;
            pedi2.Sala = "E355";
            pedi2.DataVigilancia = new DateTime(2018, 07, 20);
            pedi2.HoraVigilancia = new DateTime(2018, 07, 20, 21, 30, 00);
            pedi2.ConfirmarVigia = true;
            pedi2.Motivo = "";
            pedi2.TipoEstado = Estado.Confirmado;

            pedido[1] = pedi2;
            dbContext.Add(pedi2);

            PedidoVigilancia pedi3 = new PedidoVigilancia();

            pedi3.IdPedido = 125;
            pedi3.NumeroDocente = "2010";
            pedi3.PrimeiroNome = "Diogo";
            pedi3.UltimoNome = "Rodrigues";
            pedi3.CursoId = 1;
            pedi3.UCid = 1;
            pedi3.Sala = "D205";
            pedi3.DataVigilancia = new DateTime(2018, 10, 05);
            pedi3.HoraVigilancia = new DateTime(2018, 10, 05, 12, 30, 00);
            pedi3.ConfirmarVigia = true;
            pedi3.Motivo = "";
            pedi3.TipoEstado = Estado.EmEspera;

            pedido[2] = pedi3;
            dbContext.Add(pedi3);

            dbContext.SaveChanges();

            return pedido;
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
