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
    public class FixedAssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FixedAssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FixedAssets
        public async Task<IActionResult> Index()
        {
            return View(await _context.FixedAssets.ToListAsync());
        }

        // GET: FixedAssets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssets = await _context.FixedAssets
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fixedAssets == null)
            {
                return NotFound();
            }

            return View(fixedAssets);
        }

        // GET: FixedAssets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FixedAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOfBuy,DateOfUseStart,Name,Identfier,OriginalValue,DepreciationRate,UpgradeValue,UpdatedOriginalValue,LiquidationDate,LiquidationReason")] FixedAssets fixedAssets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fixedAssets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fixedAssets);
        }

        // GET: FixedAssets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssets = await _context.FixedAssets.SingleOrDefaultAsync(m => m.Id == id);
            if (fixedAssets == null)
            {
                return NotFound();
            }
            return View(fixedAssets);
        }

        // POST: FixedAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfBuy,DateOfUseStart,Name,Identfier,OriginalValue,DepreciationRate,UpgradeValue,UpdatedOriginalValue,LiquidationDate,LiquidationReason")] FixedAssets fixedAssets)
        {
            if (id != fixedAssets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixedAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixedAssetsExists(fixedAssets.Id))
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
            return View(fixedAssets);
        }

        // GET: FixedAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssets = await _context.FixedAssets
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fixedAssets == null)
            {
                return NotFound();
            }

            return View(fixedAssets);
        }

        // POST: FixedAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedAssets = await _context.FixedAssets.SingleOrDefaultAsync(m => m.Id == id);
            _context.FixedAssets.Remove(fixedAssets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedAssetsExists(int id)
        {
            return _context.FixedAssets.Any(e => e.Id == id);
        }
    }
}
