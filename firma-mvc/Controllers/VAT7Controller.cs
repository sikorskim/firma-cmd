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
    public class VAT7Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public VAT7Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VAT7
        public async Task<IActionResult> Index()
        {
            return View(await _context.VAT7.ToListAsync());
        }

        // GET: VAT7/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vAT7 = await _context.VAT7
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vAT7 == null)
            {
                return NotFound();
            }

            return View(vAT7);
        }

        // GET: VAT7/Create
        public IActionResult Create()
        {
            VAT7 vat7 = new VAT7();
            DateTime currDate = DateTime.Now;
            vat7.Year = currDate.Year;
            vat7.Month = currDate.Month - 1;
            vat7.Paid = false;
            vat7.Value = vat7.compute(_context);

            return View(vat7);
        }

        // POST: VAT7/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Month,Paid,Value")] VAT7 vAT7)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vAT7);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vAT7);
        }

        // GET: VAT7/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vAT7 = await _context.VAT7.FindAsync(id);
            if (vAT7 == null)
            {
                return NotFound();
            }
            return View(vAT7);
        }

        // POST: VAT7/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Month,Paid,Value")] VAT7 vAT7)
        {
            if (id != vAT7.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vAT7);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VAT7Exists(vAT7.Id))
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
            return View(vAT7);
        }

        // GET: VAT7/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vAT7 = await _context.VAT7
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vAT7 == null)
            {
                return NotFound();
            }

            return View(vAT7);
        }

        // POST: VAT7/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vAT7 = await _context.VAT7.FindAsync(id);
            _context.VAT7.Remove(vAT7);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VAT7Exists(int id)
        {
            return _context.VAT7.Any(e => e.Id == id);
        }
    }
}
