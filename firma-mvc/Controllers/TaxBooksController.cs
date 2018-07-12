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
    public class TaxBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxBookItem.Include(i=>i.Contractor).ToListAsync());
        }

        // GET: TaxBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxBook = await _context.TaxBookItem.Include(i=>i.Contractor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taxBook == null)
            {
                return NotFound();
            }

            return View(taxBook);
        }

        // GET: TaxBooks/Create
        public IActionResult Create()
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            return View();
        }

        // POST: TaxBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,InvoiceNumber,ContractorId,Description,SellValue,OtherIncome,GoodsBuys,BuysSideEffects,Salary,OtherCosts,Column15,CostDescription,ResearchCostValue,Comments")] TaxBook taxBook)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");

            if (ModelState.IsValid)
            {
                _context.Add(taxBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxBook);
        }

        // GET: TaxBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }

            var taxBook = await _context.TaxBookItem.SingleOrDefaultAsync(m => m.Id == id);
            if (taxBook == null)
            {
                return NotFound();
            }
            return View(taxBook);
        }

        // POST: TaxBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,InvoiceNumber,ContractorId,Description,SellValue,OtherIncome,GoodsBuys,BuysSideEffects,Salary,OtherCosts,Column15,CostDescription,ResearchCostValue,Comments")] TaxBook taxBook)
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");

            if (id != taxBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxBookExists(taxBook.Id))
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
            return View(taxBook);
        }

        // GET: TaxBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxBook = await _context.TaxBookItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taxBook == null)
            {
                return NotFound();
            }

            return View(taxBook);
        }

        // POST: TaxBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxBook = await _context.TaxBookItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.TaxBookItem.Remove(taxBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxBookExists(int id)
        {
            return _context.TaxBookItem.Any(e => e.Id == id);
        }
    }
}
