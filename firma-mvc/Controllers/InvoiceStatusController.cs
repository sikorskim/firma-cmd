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
    public class InvoiceStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoiceStatus.ToListAsync());
        }

        // GET: InvoiceStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatus = await _context.InvoiceStatus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceStatus == null)
            {
                return NotFound();
            }

            return View(invoiceStatus);
        }

        // GET: InvoiceStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] InvoiceStatus invoiceStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceStatus);
        }

        // GET: InvoiceStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatus = await _context.InvoiceStatus.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceStatus == null)
            {
                return NotFound();
            }
            return View(invoiceStatus);
        }

        // POST: InvoiceStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] InvoiceStatus invoiceStatus)
        {
            if (id != invoiceStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceStatusExists(invoiceStatus.Id))
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
            return View(invoiceStatus);
        }

        // GET: InvoiceStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatus = await _context.InvoiceStatus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceStatus == null)
            {
                return NotFound();
            }

            return View(invoiceStatus);
        }

        // POST: InvoiceStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceStatus = await _context.InvoiceStatus.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceStatus.Remove(invoiceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceStatusExists(int id)
        {
            return _context.InvoiceStatus.Any(e => e.Id == id);
        }
    }
}
