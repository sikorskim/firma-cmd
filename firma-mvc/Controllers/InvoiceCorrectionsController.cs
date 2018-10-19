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
    public class InvoiceCorrectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceCorrectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceCorrections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceCorrections.Include(i => i.Contractor).Include(i => i.InvoiceStatus).Include(i => i.PaymentMethod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceCorrections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceCorrection = await _context.InvoiceCorrections
                .Include(i => i.Contractor)
                .Include(i => i.InvoiceStatus)
                .Include(i => i.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceCorrection == null)
            {
                return NotFound();
            }

            return View(invoiceCorrection);
        }

        // GET: InvoiceCorrections/Create
        public IActionResult Create()
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Discriminator");
            ViewData["InvoiceStatusId"] = new SelectList(_context.InvoiceStatus, "Id", "Id");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id");
            return View();
        }

        // POST: InvoiceCorrections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,DateOfCorrection,CorrectionCause,Id,Number,DateOfIssue,DateOfDelivery,ContractorId,PaymentMethodId,InvoiceStatusId")] InvoiceCorrection invoiceCorrection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceCorrection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Discriminator", invoiceCorrection.ContractorId);
            ViewData["InvoiceStatusId"] = new SelectList(_context.InvoiceStatus, "Id", "Id", invoiceCorrection.InvoiceStatusId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id", invoiceCorrection.PaymentMethodId);
            return View(invoiceCorrection);
        }

        // GET: InvoiceCorrections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceCorrection = await _context.InvoiceCorrections.FindAsync(id);
            if (invoiceCorrection == null)
            {
                return NotFound();
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Discriminator", invoiceCorrection.ContractorId);
            ViewData["InvoiceStatusId"] = new SelectList(_context.InvoiceStatus, "Id", "Id", invoiceCorrection.InvoiceStatusId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id", invoiceCorrection.PaymentMethodId);
            return View(invoiceCorrection);
        }

        // POST: InvoiceCorrections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,DateOfCorrection,CorrectionCause,Id,Number,DateOfIssue,DateOfDelivery,ContractorId,PaymentMethodId,InvoiceStatusId")] InvoiceCorrection invoiceCorrection)
        {
            if (id != invoiceCorrection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceCorrection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceCorrectionExists(invoiceCorrection.Id))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Discriminator", invoiceCorrection.ContractorId);
            ViewData["InvoiceStatusId"] = new SelectList(_context.InvoiceStatus, "Id", "Id", invoiceCorrection.InvoiceStatusId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id", invoiceCorrection.PaymentMethodId);
            return View(invoiceCorrection);
        }

        // GET: InvoiceCorrections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceCorrection = await _context.InvoiceCorrections
                .Include(i => i.Contractor)
                .Include(i => i.InvoiceStatus)
                .Include(i => i.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceCorrection == null)
            {
                return NotFound();
            }

            return View(invoiceCorrection);
        }

        // POST: InvoiceCorrections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceCorrection = await _context.InvoiceCorrections.FindAsync(id);
            _context.InvoiceCorrections.Remove(invoiceCorrection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceCorrectionExists(int id)
        {
            return _context.InvoiceCorrections.Any(e => e.Id == id);
        }
    }
}
