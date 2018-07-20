using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using firma_mvc;
using firma_mvc.Data;

namespace firma_mvc.Controllers
{
    public class IncomeTaxesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomeTaxesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncomeTaxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeTax.ToListAsync());
        }

        // GET: IncomeTaxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeTax = await _context.IncomeTax
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeTax == null)
            {
                return NotFound();
            }

            return View(incomeTax);
        }

        // GET: IncomeTaxes/Create
        public IActionResult Create()
        {
            IncomeTax incomeTax = new IncomeTax();
            DateTime currDate = DateTime.Now;
            incomeTax.Year = currDate.Year;
            incomeTax.Month = currDate.Month - 1;
            incomeTax.Paid = false;

            decimal income = (decimal)_context.VATRegisterSell.Where(p=>p.Month==incomeTax.Month && p.Year==incomeTax.Year).Sum(p=>p.ValueNetto23);
            decimal costs = (decimal)_context.VATRegisterBuy.Where(p => p.Month == incomeTax.Month && p.Year == incomeTax.Year).Sum(p => p.ValueNetto);
            incomeTax.Value = incomeTax.compute(income, costs);

            return View(incomeTax);
        }

        // POST: IncomeTaxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Month,Paid,Value")] IncomeTax incomeTax)
        {
            if (ModelState.IsValid)
            {
                if (incomeTax.Value == 0)
                {
                    decimal income = (decimal)_context.VATRegisterSell.Where(p => p.Month == incomeTax.Month && p.Year == incomeTax.Year).Sum(p => p.ValueNetto23);
                    decimal costs = (decimal)_context.VATRegisterBuy.Where(p => p.Month == incomeTax.Month && p.Year == incomeTax.Year).Sum(p => p.ValueNetto);
                    incomeTax.Value = incomeTax.compute(income, costs);
                }
                _context.Add(incomeTax);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomeTax);
        }

        // GET: IncomeTaxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeTax = await _context.IncomeTax.FindAsync(id);
            if (incomeTax == null)
            {
                return NotFound();
            }
            return View(incomeTax);
        }

        // POST: IncomeTaxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Month,Paid,Value")] IncomeTax incomeTax)
        {
            if (id != incomeTax.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeTax);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeTaxExists(incomeTax.Id))
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
            return View(incomeTax);
        }

        // GET: IncomeTaxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeTax = await _context.IncomeTax
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeTax == null)
            {
                return NotFound();
            }

            return View(incomeTax);
        }

        // POST: IncomeTaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomeTax = await _context.IncomeTax.FindAsync(id);
            _context.IncomeTax.Remove(incomeTax);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeTaxExists(int id)
        {
            return _context.IncomeTax.Any(e => e.Id == id);
        }
    }
}
