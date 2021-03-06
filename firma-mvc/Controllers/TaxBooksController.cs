﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using firma_mvc;
using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace firma_mvc.Controllers
{
    public class TaxBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxBooksController (ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxBooks
        public async Task<IActionResult> Index (int? year, int? month)
        {
            ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", DateTime.Now.Month);
            ViewData["Year"] = new SelectList (Tools.getYearsList (), DateTime.Now.Year);
            ViewData["SelectedMonth"] = DateTime.Now.Month;
            ViewData["SelectedYear"] = DateTime.Now.Year;

            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            TaxBook taxBook = new TaxBook ();
            taxBook.Date = DateTime.Now.Date;
            ViewData["TaxBook"] = taxBook;

            var applicationDbContext = _context.TaxBookItem.Include (i => i.Contractor);

            if (month != null)
            {
                var filteredResult = applicationDbContext.Where (p => p.Date.Month == month);
                ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", month);
                ViewData["SelectedMonth"] = month;

                if (year != null)
                {
                    filteredResult = applicationDbContext.Where (p => p.Date.Month == month && p.Date.Year == year);
                    ViewData["Year"] = new SelectList (Tools.getYearsList (), year);
                    ViewData["SelectedYear"] = year;
                }
                return View (await filteredResult.OrderBy(p=>p.Date).ToListAsync ());
            }
            else
            {
                return View (await applicationDbContext.Where (p => p.Date.Year == DateTime.Now.Year && p.Date.Month == DateTime.Now.Month).OrderBy(p=>p.Date).ToListAsync ());
            }
        }

        // GET: TaxBooks/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var taxBook = await _context.TaxBookItem.Include (i => i.Contractor)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (taxBook == null)
            {
                return NotFound ();
            }

            return View (taxBook);
        }

        // GET: TaxBooks/Create
        public IActionResult CreatePartial ()
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            return PartialView ();
        }

        // POST: TaxBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,Date,InvoiceNumber,ContractorId,Description,SellValue,OtherIncome,GoodsBuys,BuysSideEffects,Salary,OtherCosts,Column15,CostDescription,ResearchCostValue,Comments")] TaxBook taxBook)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");

            if (ModelState.IsValid)
            {
                _context.Add (taxBook);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (taxBook);
        }

        // GET: TaxBooks/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");

            if (id == null)
            {
                return NotFound ();
            }

            var taxBook = await _context.TaxBookItem.SingleOrDefaultAsync (m => m.Id == id);
            if (taxBook == null)
            {
                return NotFound ();
            }
            return View (taxBook);
        }

        // POST: TaxBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Date,InvoiceNumber,ContractorId,Description,SellValue,OtherIncome,GoodsBuys,BuysSideEffects,Salary,OtherCosts,Column15,CostDescription,ResearchCostValue,Comments")] TaxBook taxBook)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");

            if (id != taxBook.Id)
            {
                return NotFound ();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (taxBook);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxBookExists (taxBook.Id))
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index), "TaxBooks", new { month = taxBook.Date.Month, year = taxBook.Date.Year });
            }
            return View (taxBook);
        }

        // GET: TaxBooks/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var taxBook = await _context.TaxBookItem
                .SingleOrDefaultAsync (m => m.Id == id);
            if (taxBook == null)
            {
                return NotFound ();
            }

            return View (taxBook);
        }

        // POST: TaxBooks/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var taxBook = await _context.TaxBookItem.SingleOrDefaultAsync (m => m.Id == id);
            _context.TaxBookItem.Remove (taxBook);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool TaxBookExists (int id)
        {
            return _context.TaxBookItem.Any (e => e.Id == id);
        }

        public IActionResult GetPdfFile (string filename, string downloadFilename)
        {
            const string contentType = "application/pdf";
            HttpContext.Response.ContentType = contentType;
            FileContentResult result = null;
            filename = "tmp/" + filename;

            try
            {
                result = new FileContentResult (System.IO.File.ReadAllBytes (filename), contentType)
                {
                    FileDownloadName = downloadFilename + ".pdf"
                };
                Tools.deleteTempFiles (filename.Substring (4, 64));
                return result;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine (e.Message);
                return NotFound ();
            }
        }

        public async Task<IActionResult> GenerateTaxBook (int? year, int? month)
        {
            if (year == null || month == null)
            {
                return NotFound ();
            }

            var taxBook = new TaxBook ();
            string pdfFilename = taxBook.generate (_context, (int) year, (int) month);
            string downFilename = taxBook.getDownloadFilename ((int) year, (int) month);
            await Task.Delay (1000);
            return RedirectToAction ("GetPdfFile", new { filename = pdfFilename, downloadFilename = downFilename });
        }
    }
}