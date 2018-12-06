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
    public class CarCostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarCostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {            
            return View(await _context.CarCost.Include(i=>i.CarCostType).ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCost = await _context.CarCost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carCost == null)
            {
                return NotFound();
            }

            return View(carCost);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {
            ViewData["CarCostTypeId"] = new SelectList (_context.CarCostType, "Id", "Name");
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Price,Description,CarCostTypeId")] CarCost carCost)
        {
            ViewData["CarCostTypeId"] = new SelectList (_context.CarCostType, "Id", "Name");
            if (ModelState.IsValid)
            {
                _context.Add(carCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carCost);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCost = await _context.CarCost.FindAsync(id);
            if (carCost == null)
            {
                return NotFound();
            }
            return View(carCost);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Price,Description,CarCostTypeId")] CarCost carCost)
        {
            if (id != carCost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {                
                _context.Update(carCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carCost);
        }        

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCost = await _context.CarCost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carCost == null)
            {
                return NotFound();
            }

            return View(carCost);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carCost = await _context.CarCost.FindAsync(id);
            _context.CarCost.Remove(carCost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}