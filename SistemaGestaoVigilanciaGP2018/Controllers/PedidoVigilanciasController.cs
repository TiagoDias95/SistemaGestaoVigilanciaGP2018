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

namespace SistemaGestaoVigilanciaGP2018.Controllers
{
    public class PedidoVigilanciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;



        public PedidoVigilanciasController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: PedidoVigilancias


       [Authorize(Roles = "Utilizador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PedidoVigilancia.ToListAsync());
        }

        // GET: PedidoVigilancias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoVigilancia = await _context.PedidoVigilancia
                .SingleOrDefaultAsync(m => m.PrimeiroNome == id);
            if (pedidoVigilancia == null)
            {
                return NotFound();
            }

            return View(pedidoVigilancia);
        }

        // GET: PedidoVigilancias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PedidoVigilancias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrimeiroNome,UltimoNome,NumeroDocente,UnidadeCurricular,DataVigilancia")] PedidoVigilancia pedidoVigilancia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoVigilancia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(pedidoVigilancia);
        }

        // GET: PedidoVigilancias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoVigilancia = await _context.PedidoVigilancia.SingleOrDefaultAsync(m => m.PrimeiroNome == id);
            if (pedidoVigilancia == null)
            {
                return NotFound();
            }
            return View(pedidoVigilancia);
        }

        // POST: PedidoVigilancias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PrimeiroNome,UltimoNome,NumeroDocente,UnidadeCurricular,DataVigilancia")] PedidoVigilancia pedidoVigilancia)
        {
            if (id != pedidoVigilancia.PrimeiroNome)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoVigilancia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoVigilanciaExists(pedidoVigilancia.PrimeiroNome))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedidoVigilancia);
        }

        // GET: PedidoVigilancias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoVigilancia = await _context.PedidoVigilancia
                .SingleOrDefaultAsync(m => m.PrimeiroNome == id);
            if (pedidoVigilancia == null)
            {
                return NotFound();
            }

            return View(pedidoVigilancia);
        }

        // POST: PedidoVigilancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pedidoVigilancia = await _context.PedidoVigilancia.SingleOrDefaultAsync(m => m.PrimeiroNome == id);
            _context.PedidoVigilancia.Remove(pedidoVigilancia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


          private bool PedidoVigilanciaExists(string id)
        {
            return _context.PedidoVigilancia.Any(e => e.PrimeiroNome == id);
        }

        [HttpGet]
        //[Authorize(Roles = "Estudante")]
        public IActionResult FazerPedido()
        {

            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> FazerPedido(PedidoVigilancia model)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var pedido = new PedidoVigilancia { IdPedido = model.IdPedido, NumeroDocente = model.NumeroDocente, PrimeiroNome = model.PrimeiroNome, UltimoNome = model.UltimoNome, DataVigilancia = model.DataVigilancia, UnidadeCurricular = model.UnidadeCurricular };      
            _context.Add(pedido);
            await _context.SaveChangesAsync();

            var user = _context.Users.Where(u => u.NumeroDocente == pedido.NumeroDocente)
             .Select(u => new {
              ID = u.Id,
               FirstName = u.PrimeiroNome,
              LastName = u.UltimoNome,
              email = u.Email
                   }).Single();

            ////var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.VigiaLink(user.ID, "", Request.Scheme);
            var link = "http://localhost:64078/PedidoVigilancias/VigiaRecusa";
            await _emailSender.SendEmailAsync(user.email,"Pedido de Vigilancia - EST" ,$"Por favor confirme o pedido de vigilancia ao clicar no seguinte link: < a href = '{HtmlEncoder.Default.Encode(link)}' > link </ a > ");


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Utilizador")]
        public IActionResult VigiaRecusa()
        {
            return View();
        }


    }
}
