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
    public class VATRegisterSellsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VATRegisterSellsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VATRegisterSells
        public async Task<IActionResult> Index(int? year, int? month)
        {
            ViewData["Month"] = new SelectList(Tools.getMonthsDictionary(), "Key", "Value", DateTime.Now.Month);
            ViewData["Year"] = new SelectList(Tools.getYearsList(), DateTime.Now.Year);
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            VATRegisterSell vATRegisterSell = new VATRegisterSell(DateTime.Now);
            ViewData["VATRegisterSell"] = vATRegisterSell;

            var applicationDbContext = _context.VATRegisterSell.Include(i => i.Contractor);

            if (month != null)
            {
                var filteredResult = applicationDbContext.Where(p => p.DateOfIssue.Month == month);
                ViewData["Month"] = new SelectList(Tools.getMonthsDictionary(), "Key", "Value", month);
                if (year != null)
                {
                    filteredResult = applicationDbContext.Where(p => p.DateOfIssue.Month == month && p.DateOfIssue.Year == year);
                    ViewData["Year"] = new SelectList(Tools.getYearsList(), year);
                }
                return View(await filteredResult.ToListAsync());
            }
            else
            {
                return View(await applicationDbContext.Where(p=>p.Month==DateTime.Now.Month && p.Year==DateTime.Now.Year).ToListAsync());
            }
        }

        // GET: VATRegisterSells/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vATRegisterSell = await _context.VATRegisterSell
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vATRegisterSell == null)
            {
                return NotFound();
            }

            return View(vATRegisterSell);
        }

        // GET: VATRegisterSells/Create
        public IActionResult Create()
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            return View();
        }

        // POST: VATRegisterSells/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeliveryDate,DateOfIssue,DocumentNumber,Contractor,ValueBrutto,ValueNetto23,VATValue23,ValueNetto7_8,VATValue7_8,ValueNetto3_5,VATValue3_5,ValueNetto0,ValueTaxFree,ValueNoTax")] VATRegisterSell vATRegisterSell)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            if (ModelState.IsValid)
            {
                vATRegisterSell.Number = vATRegisterSell.getOrderNumber(_context);
                _context.Add(vATRegisterSell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vATRegisterSell);
        }

        // GET: VATRegisterSells/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var vATRegisterSell = await _context.VATRegisterSell.SingleOrDefaultAsync(m => m.Id == id);
            if (vATRegisterSell == null)
            {
                return NotFound();
            }
            return View(vATRegisterSell);
        }

        // POST: VATRegisterSells/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DeliveryDate,DateOfIssue,DocumentNumber,Contractor,ValueBrutto,ValueNetto23,VATValue23,ValueNetto7_8,VATValue7_8,ValueNetto3_5,VATValue3_5,ValueNetto0,ValueTaxFree,ValueNoTax")] VATRegisterSell vATRegisterSell)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            if (id != vATRegisterSell.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vATRegisterSell);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VATRegisterSellExists(vATRegisterSell.Id))
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
            return View(vATRegisterSell);
        }

        // GET: VATRegisterSells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vATRegisterSell = await _context.VATRegisterSell
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vATRegisterSell == null)
            {
                return NotFound();
            }

            return View(vATRegisterSell);
        }

        // POST: VATRegisterSells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vATRegisterSell = await _context.VATRegisterSell.SingleOrDefaultAsync(m => m.Id == id);
            _context.VATRegisterSell.Remove(vATRegisterSell);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VATRegisterSellExists(int id)
        {
            return _context.VATRegisterSell.Any(e => e.Id == id);
        }
    }
}
