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
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;



        public PedidoVigilanciasController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            //_userManager = user;
            _emailSender = emailSender;
        }

        // GET: PedidoVigilancias


        [Authorize(Roles = "Utilizador,Administrador")]
        public async Task<IActionResult> Index()
        {

            return View(await _context.PedidoVigilancia.ToListAsync());
        }

        // GET: PedidoVigilancias/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var pedidoVigilancia = await _context.PedidoVigilancia
                .SingleOrDefaultAsync(m => m.IdPedido == id);
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != null)
            {
                CursoDropDownList(id);
                UCDropDownList(id);
            }
            else
            {
                CursoDropDownList();
                UCDropDownList();
            }

            if (id == 0)
            {
                return NotFound();
            }

            var pedidoVigilancia = await _context.PedidoVigilancia.SingleOrDefaultAsync(m => m.IdPedido == id);
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("PrimeiroNome,UltimoNome,NumeroDocente,UnidadeCurricular,DataVigilancia")] PedidoVigilancia pedidoVigilancia)
        {
            if (id != pedidoVigilancia.IdPedido)
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
                    if (!PedidoVigilanciaExists(pedidoVigilancia.IdPedido))
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
            CursoDropDownList(pedidoVigilancia.CursoId);
            UCDropDownList(pedidoVigilancia.UCid);
            return View(pedidoVigilancia);
        }

        // GET: PedidoVigilancias/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var pedidoVigilancia = await _context.PedidoVigilancia
                .SingleOrDefaultAsync(m => m.IdPedido == id);
            if (pedidoVigilancia == null)
            {
                return NotFound();
            }

            return View(pedidoVigilancia);
        }

        // POST: PedidoVigilancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoVigilancia = await _context.PedidoVigilancia.SingleOrDefaultAsync(m => m.IdPedido == id);
            _context.PedidoVigilancia.Remove(pedidoVigilancia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


          private bool PedidoVigilanciaExists(int id)
        {
            return _context.PedidoVigilancia.Any(e => e.IdPedido == id);
        }

        //public IActionResult GetCurso()
        //{
        //    var curso = _context.Curso.OrderBy(c => c.NomeCurso).Select(x => new { Id = x.IdC, Value = x.NomeCurso });
        //    var model = new PedidoVigilancia();
        //    model.CursoList = new SelectList(curso, "Id", "Value");

        //    return View(model);
        //}
        //public IActionResult GetUC()
        //{
        //    var uc = _context.UnidadeCurricular.OrderBy(c => c.NomeUC).Select(x => new { Id = x.IdUC, Value = x.NomeUC });
        //    var model = new PedidoVigilancia();
        //    model.UCList = new SelectList(uc, "Id", "Value");

        //    return View(model);
        //}

        [HttpGet]
        [Authorize(Roles = "Administrador,Utilizador")]
        public IActionResult FazerPedido(int? id)
        {
            if (id != null)
            {
                CursoDropDownList(id);
                UCDropDownList(id);
            }
            else
            {
                CursoDropDownList();
                UCDropDownList();
            }

            //ViewData["Curso"] = _context.Curso.ToList();
            //ViewData["UnidadeCurricular"] = _context.UnidadeCurricular.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Utilizador")]
        public async Task<IActionResult> FazerPedido(PedidoVigilancia model)
        {

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var count =0;
            var est = Estado.EmEspera;

            var cur = _context.Curso.Where(u => u.IdC == model.CursoId)
           .Select(u => new {
               ID = u.IdC,
               Nome = u.NomeCurso              
           }).Single();

            var uc = _context.UnidadeCurricular.Where(u => u.IdUC == model.UCid)
          .Select(u => new {
              ID = u.IdUC,
              Nome = u.NomeUC
          }).Single();



            var pedido = new PedidoVigilancia {

                IdPedido = model.IdPedido,
                NumeroDocente = model.NumeroDocente,
                PrimeiroNome = model.PrimeiroNome,
                UltimoNome = model.UltimoNome,
                UCList = model.UCList,
                CursoList = model.CursoList,
                Curso = cur.Nome,
                UC = uc.Nome,
                DataVigilancia = model.DataVigilancia,
                HoraVigilancia = model.HoraVigilancia,
                Sala = model.Sala,
                TipoEstado = model.TipoEstado
            };

            pedido.TipoEstado = est;

            var user = _context.Users.Where(u => u.NumeroDocente == pedido.NumeroDocente)
              .Select(u => new
              {
                  ID = u.Id,
                  nudo = u.NumeroDocente,
                  email = u.Email,
                  nume = u.NumeroVigias
              }).FirstOrDefault();

            var counter = (from o in _context.PedidoVigilancia
                           where o.NumeroDocente == user.nudo
                           from t in o.NumeroDocente
                           select t).Count();
            foreach (var p in _context.PedidoVigilancia)
            {
                foreach (var u in _context.Users)
                {
                    if (p.NumeroDocente == u.NumeroDocente)
                    {
                        u.NumeroVigias = count++;
                        //p.TipoEstado = est;
                    }
                }
            }
                      


            //if (ModelState.IsValid) { 
            _context.Add(pedido);
            await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));

            //}
     
            ////var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.VigiaLink(user.ID, "", Request.Scheme);
            var link = "http://http://sistemagestaovigilanciagp.azurewebsites.net/PedidoVigilancias/VigiaRecusa";
            await _emailSender.SendEmailAsync(user.email,"Pedido de Vigilancia - EST" ,$"Por favor confirme o pedido de vigilancia ao clicar no seguinte link: < a href = '{HtmlEncoder.Default.Encode(link)}' > link </ a > ");

            CursoDropDownList(pedido.CursoId);
            UCDropDownList(pedido.UCid);

            return RedirectToAction(nameof(Index));


        }


        [HttpGet]
        [Authorize(Roles = "Utilizador,Administrador")]
        public IActionResult VigiaRecusa()
        {
           
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Utilizador,Administrador")]
        public IActionResult VigiaRecusa(ApplicationUser model)
        {
            var user = _context.PedidoVigilancia.Where(u => u.NumeroDocente == model.NumeroDocente)
           .Select(u => new
           {
              ConfirmarVigia = u.ConfirmarVigia,
              Motivo = u.Motivo
           }).FirstOrDefault();


            var counter = (from o in _context.PedidoVigilancia
                           where o.NumeroDocente == model.NumeroDocente
                           from t in o.NumeroDocente
                           select t).Count();


            foreach (var p in _context.PedidoVigilancia)
            {
                foreach (var u in _context.Users)
                {
                    if (p.NumeroDocente == u.NumeroDocente)
                    {
                        if (user.ConfirmarVigia == true)
                        {
                            p.TipoEstado = Estado.Confirmado;
                        }

                        p.TipoEstado = Estado.recusado;

                    }

                }
            }


            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Utilizador,Administrador")]
        public async Task<IActionResult> ListaDocenteVigias()
        {

            return View(await _context.Users.ToListAsync());
        }



        private void CursoDropDownList(object selectedCurso = null)
        {
            var curso = from p in _context.Curso
                                     orderby p.NomeCurso
                                     select p;
            ViewBag.CursoId = new SelectList(curso.AsNoTracking(), "IdC", "NomeCurso", selectedCurso);
        }

        private void UCDropDownList(object selectedUC = null)
        {
            var uc = from p in _context.UnidadeCurricular
                                        orderby p.NomeUC
                                         select p;
            ViewBag.UCid = new SelectList(uc.AsNoTracking(), "IdUC", "NomeUC", selectedUC);
        }

    }
}
