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
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceHeaders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceHeader.Include(i => i.Contractor).Include(i => i.PaymentMethod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceHeader = await _context.InvoiceHeader
                .Include(i => i.Contractor)
                .Include(i => i.PaymentMethod)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceHeader == null)
            {
                return NotFound();
            }
            
            return View(invoiceHeader);
        }
        
        // GET: InvoiceHeaders/Create
        public IActionResult Create()
        {
            Invoice invoice = new Invoice();
            invoice.Number = getNumber();
            invoice.DateOfIssue=DateTime.Now;
            
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name");
            return View(invoice);
        }

        // POST: InvoiceHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DateOfIssue,ContractorId,PaymentMethodId,ItemsCount,TotalValue,TotalValueInclVat")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name", invoice.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name", invoice.PaymentMethodId);
            return View(invoice);
        }

        // GET: InvoiceHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceHeader = await _context.InvoiceHeader.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceHeader == null)
            {
                return NotFound();
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Id", invoiceHeader.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id", invoiceHeader.PaymentMethodId);
            return View(invoiceHeader);
        }

        // POST: InvoiceHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DateOfIssue,ContractorId,PaymentMethodId,ItemsCount,TotalValue,TotalValueInclVat")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceHeaderExists(invoice.Id))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Id", invoice.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id", invoice.PaymentMethodId);
            return View(invoice);
        }

        // GET: InvoiceHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceHeader = await _context.InvoiceHeader
                .Include(i => i.Contractor)
                .Include(i => i.PaymentMethod)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceHeader == null)
            {
                return NotFound();
            }

            return View(invoiceHeader);
        }

        // POST: InvoiceHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceHeader = await _context.InvoiceHeader.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceHeader.Remove(invoiceHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceHeaderExists(int id)
        {
            return _context.InvoiceHeader.Any(e => e.Id == id);
        }
        
        public string getNumber()
        {
            string number=string.Empty;            
            
            try
            {
                string lastNumber = _context.InvoiceHeader.Last(p => p.DateOfIssue.Year == DateTime.Now.Year).Number;                
                int nextNumber = Int32.Parse(lastNumber.Substring(0, lastNumber.IndexOf('/')));
                nextNumber++;
                number = nextNumber.ToString()+"/" + DateTime.Now.Year;
            }
            catch (Exception ex)
            {
                number = "1/" + DateTime.Now.Year;
            }
            
            return number;
        }
    }
}
