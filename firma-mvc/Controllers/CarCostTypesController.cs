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
    public class CarCostTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarCostTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarCostType.ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCostType = await _context.CarCostType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carCostType == null)
            {
                return NotFound();
            }

            return View(carCostType);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] CarCostType carCostType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carCostType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carCostType);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCostType = await _context.CarCostType.FindAsync(id);
            if (carCostType == null)
            {
                return NotFound();
            }
            return View(carCostType);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] CarCostType carCostType)
        {
            if (id != carCostType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {                
                _context.Update(carCostType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carCostType);
        }        

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCostType = await _context.CarCostType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carCostType == null)
            {
                return NotFound();
            }

            return View(carCostType);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carCostType = await _context.CarCostType.FindAsync(id);
            _context.CarCostType.Remove(carCostType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}