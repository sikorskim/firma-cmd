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
    public class VATRegisterBuysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VATRegisterBuysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VATRegisterBuys
        public async Task<IActionResult> Index()
        {
            return View(await _context.VATRegisterBuy.Include(i=>i.Contractor).ToListAsync());
        }

        // GET: VATRegisterBuys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vATRegisterBuy = await _context.VATRegisterBuy
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vATRegisterBuy == null)
            {
                return NotFound();
            }

            return View(vATRegisterBuy);
        }

        // GET: VATRegisterBuys/Create
        public IActionResult Create()
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            VATRegisterBuy vATRegisterBuy = new VATRegisterBuy();
            DateTime currDate = DateTime.Now;
            vATRegisterBuy.DateOfIssue = currDate;
            vATRegisterBuy.DeliveryDate = currDate;
            vATRegisterBuy.Month = currDate.Month;
            vATRegisterBuy.Year = currDate.Year;
            return View(vATRegisterBuy);
        }

        // POST: VATRegisterBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DeliveryDate,DateOfIssue,DocumentNumber,ContractorId,ValueBrutto,ValueNetto,TaxDeductibleValue,TaxFreeBuysValue,NoTaxDeductibleBuysValue,Month,Year")] VATRegisterBuy vATRegisterBuy)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            if (ModelState.IsValid)
            {
                _context.Add(vATRegisterBuy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vATRegisterBuy);
        }

        // GET: VATRegisterBuys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vATRegisterBuy = await _context.VATRegisterBuy.SingleOrDefaultAsync(m => m.Id == id);
            if (vATRegisterBuy == null)
            {
                return NotFound();
            }
            return View(vATRegisterBuy);
        }

        // POST: VATRegisterBuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DeliveryDate,DateOfIssue,DocumentNumber,Contractor,ValueBrutto,ValueNetto,TaxDeductibleValue,TaxFreeBuysValue,NoTaxDeductibleBuysValue")] VATRegisterBuy vATRegisterBuy)
        {
            if (id != vATRegisterBuy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vATRegisterBuy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VATRegisterBuyExists(vATRegisterBuy.Id))
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
            return View(vATRegisterBuy);
        }

        // GET: VATRegisterBuys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vATRegisterBuy = await _context.VATRegisterBuy
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vATRegisterBuy == null)
            {
                return NotFound();
            }

            return View(vATRegisterBuy);
        }

        // POST: VATRegisterBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vATRegisterBuy = await _context.VATRegisterBuy.SingleOrDefaultAsync(m => m.Id == id);
            _context.VATRegisterBuy.Remove(vATRegisterBuy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VATRegisterBuyExists(int id)
        {
            return _context.VATRegisterBuy.Any(e => e.Id == id);
        }
    }
}
