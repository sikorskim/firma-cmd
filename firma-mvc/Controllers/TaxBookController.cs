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
    public class TaxBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxBookItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxBookItem.ToListAsync());
        }

        // GET: TaxBookItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxBookItem = await _context.TaxBookItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taxBookItem == null)
            {
                return NotFound();
            }

            return View(taxBookItem);
        }

        // GET: TaxBookItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxBookItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,InvoiceNumber,Name,NIP,Address,Description,SellValue,OtherIncome,GoodsBuys,BuysSideEffects,Salary,OtherCosts,Column15,CostDescription,ResearchCostValue,Comments")] TaxBook taxBookItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxBookItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxBookItem);
        }

        // GET: TaxBookItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxBookItem = await _context.TaxBookItem.SingleOrDefaultAsync(m => m.Id == id);
            if (taxBookItem == null)
            {
                return NotFound();
            }
            return View(taxBookItem);
        }

        // POST: TaxBookItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,InvoiceNumber,Name,NIP,Address,Description,SellValue,OtherIncome,GoodsBuys,BuysSideEffects,Salary,OtherCosts,Column15,CostDescription,ResearchCostValue,Comments")] TaxBook taxBookItem)
        {
            if (id != taxBookItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxBookItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxBookItemExists(taxBookItem.Id))
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
            return View(taxBookItem);
        }

        // GET: TaxBookItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxBookItem = await _context.TaxBookItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taxBookItem == null)
            {
                return NotFound();
            }

            return View(taxBookItem);
        }

        // POST: TaxBookItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxBookItem = await _context.TaxBookItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.TaxBookItem.Remove(taxBookItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxBookItemExists(int id)
        {
            return _context.TaxBookItem.Any(e => e.Id == id);
        }
    }
}
