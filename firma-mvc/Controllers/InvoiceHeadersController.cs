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
    public class InvoiceHeadersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceHeadersController(ApplicationDbContext context)
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
            InvoiceHeader invoiceHeader = new InvoiceHeader();
            invoiceHeader.Number = getNumber();
            invoiceHeader.DateOfIssue=DateTime.Now;
            
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name");
            return View(invoiceHeader);
        }

        // POST: InvoiceHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DateOfIssue,ContractorId,PaymentMethodId,ItemsCount,TotalValue,TotalValueInclVat")] InvoiceHeader invoiceHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name", invoiceHeader.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name", invoiceHeader.PaymentMethodId);
            return View(invoiceHeader);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DateOfIssue,ContractorId,PaymentMethodId,ItemsCount,TotalValue,TotalValueInclVat")] InvoiceHeader invoiceHeader)
        {
            if (id != invoiceHeader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceHeaderExists(invoiceHeader.Id))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Id", invoiceHeader.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Id", invoiceHeader.PaymentMethodId);
            return View(invoiceHeader);
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
