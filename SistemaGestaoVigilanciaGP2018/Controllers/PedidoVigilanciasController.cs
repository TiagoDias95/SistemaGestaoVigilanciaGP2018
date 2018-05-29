using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoVigilanciaGP2018.Data;
using SistemaGestaoVigilanciaGP2018.Models;

namespace SistemaGestaoVigilanciaGP2018.Controllers
{
    public class PedidoVigilanciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoVigilanciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PedidoVigilancias
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
    }
}
