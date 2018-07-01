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
    public class InvoiceItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceItem.Include(i => i.Invoice);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceItem = await _context.InvoiceItem
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public IActionResult Create(int? InvoiceId, int? ItemId)
        {
            InvoiceItem invoiceItem = new InvoiceItem((int)InvoiceId);
            if (ItemId != null && ItemId!=0)
            {
                Item item = _context.Item.Include(i=>i.VAT).Include(i=>i.UnitOfMeasure).Single(p => p.Id == ItemId);
                invoiceItem.Quantity = 1;
                invoiceItem.Name = item.Name;
                invoiceItem.Price = item.Price;
                //decimal formatedPrice = decimal.Parse(item.Price.ToString(), System.Globalization.NumberStyles.Currency);
                //invoiceItem.Price = formatedPrice;

                invoiceItem.UnitOfMeasureShortName = item.UnitOfMeasure.ShortName;
                invoiceItem.VATValue = item.VAT.Value;
            }

            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name");
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceId,Quantity,Price,Name,VATValue,UnitOfMeasureShortName")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Invoices", new { id=invoiceItem.InvoiceId});
            }
            //ViewData["InvoiceId"] = new SelectList(_context.Invoice, "Id", "Discriminator", invoiceItem.InvoiceId);
            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name");
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public IActionResult CreatePopup(int? InvoiceId, int? ItemId)
        {
            InvoiceItem invoiceItem = new InvoiceItem((int)InvoiceId);
            if (ItemId != null && ItemId != 0)
            {
                Item item = _context.Item.Include(i => i.VAT).Include(i => i.UnitOfMeasure).Single(p => p.Id == ItemId);
                invoiceItem.Name = item.Name;
                invoiceItem.Price = item.Price;
                invoiceItem.UnitOfMeasureShortName = item.UnitOfMeasure.ShortName;
                invoiceItem.VATValue = item.VAT.Value;
            }

            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name");
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePopup([Bind("Id,InvoiceId,Quantity,Price,Name,VATValue,UnitOfMeasureShortName")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Invoices", new { id = invoiceItem.InvoiceId });
            }
            //ViewData["InvoiceId"] = new SelectList(_context.Invoice, "Id", "Discriminator", invoiceItem.InvoiceId);
            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name");
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceItem = await _context.InvoiceItem.FindAsync(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceId,Quantity,Price,Name,VATValue,UnitOfMeasureShortName")] InvoiceItem invoiceItem)
        {
            if (id != invoiceItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceItemExists(invoiceItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "Invoices", new { id = invoiceItem.InvoiceId });
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceItem = await _context.InvoiceItem
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceItem = await _context.InvoiceItem.FindAsync(id);
            _context.InvoiceItem.Remove(invoiceItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceItemExists(int id)
        {
            return _context.InvoiceItem.Any(e => e.Id == id);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectItem(int invoiceId, int itemId)
        {
            return RedirectToAction("Create", new { InvoiceId=invoiceId, ItemId=itemId });
        }
    }
}
