﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using firma_mvc;
using firma_mvc.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace firma_mvc.Controllers
{
    public class VATRegisterBuysController : Controller
    {
        private readonly ApplicationDbContext _context;
        string SelectedMonth = "month";
        string SelectedYear = "year";

        public VATRegisterBuysController (ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VATRegisterBuys
        public async Task<IActionResult> Index (int? year, int? month)
        {
            VATRegisterBuy vATRegisterBuy = new VATRegisterBuy ();
            vATRegisterBuy.DateOfIssue = DateTime.Now.Date;
            vATRegisterBuy.DeliveryDate = DateTime.Now.Date;
            ViewData["VATRegisterBuy"] = vATRegisterBuy;

            //test
            VATRegisterBuyViewModel vATRegisterBuyViewModel = new VATRegisterBuyViewModel();
            vATRegisterBuyViewModel.DateOfIssue = DateTime.Now.Date;
            vATRegisterBuyViewModel.DeliveryDate = DateTime.Now.Date;
            ViewData["VATRegisterBuyViewModel"] = vATRegisterBuyViewModel;
            //test end

            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", DateTime.Now.Month);
            ViewData["Year"] = new SelectList (Tools.getYearsList (), DateTime.Now.Year);

            if(HttpContext.Session.GetString(SelectedMonth)=="month" && HttpContext.Session.GetString(SelectedYear)=="year")
            {
                HttpContext.Session.SetInt32(SelectedMonth, DateTime.Now.Month);
                HttpContext.Session.SetInt32(SelectedYear, DateTime.Now.Year);
                // year = HttpContext.Session.GetInt32(SelectedYear);
                // month = HttpContext.Session.GetInt32(SelectedMonth);
            }            
            
            if(year is null && month is null)
            {
                year = HttpContext.Session.GetInt32(SelectedYear);
                month = HttpContext.Session.GetInt32(SelectedMonth);

                ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", month);
                ViewData["SelectedMonth"] = month;
                ViewData["Year"] = new SelectList (Tools.getYearsList (), year);
                ViewData["SelectedYear"] = year;
            }

            var applicationDbContext = _context.VATRegisterBuy.Include (i => i.Contractor);

            if (month != null && year != null)
            {
                HttpContext.Session.SetInt32(SelectedMonth, (int)month);
                HttpContext.Session.SetInt32(SelectedYear, (int)year);

                ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", month);
                ViewData["SelectedMonth"] = month;
                ViewData["Year"] = new SelectList (Tools.getYearsList (), year);
                ViewData["SelectedYear"] = year;

                var filteredResult = applicationDbContext.Where (p => p.DateOfIssue.Month == month && p.DateOfIssue.Year == year);

                return View (await filteredResult.OrderBy(p=>p.DateOfIssue).ToListAsync ());
            }
            else
            {
                return View (await applicationDbContext.Where (p => p.Month == DateTime.Now.Month && p.Year == DateTime.Now.Year).ToListAsync ());
            }
        }

        // GET: VATRegisterBuys/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var vATRegisterBuy = await _context.VATRegisterBuy
                .SingleOrDefaultAsync (m => m.Id == id);
            if (vATRegisterBuy == null)
            {
                return NotFound ();
            }

            return View (vATRegisterBuy);
        }

        // GET: VATRegisterBuys/Create
        public IActionResult Create ()
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            VATRegisterBuy vATRegisterBuy = new VATRegisterBuy ();
            DateTime currDate = DateTime.Now;
            vATRegisterBuy.DateOfIssue = currDate;
            vATRegisterBuy.DeliveryDate = currDate;
            vATRegisterBuy.Month = currDate.Month;
            vATRegisterBuy.Year = currDate.Year;

            return View (vATRegisterBuy);
        }

        // POST: VATRegisterBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,DeliveryDate,DateOfIssue,DocumentNumber,ContractorId,ValueBrutto,ValueNetto,TaxDeductibleValue,TaxFreeBuysValue,NoTaxDeductibleBuysValue")] VATRegisterBuy vATRegisterBuy)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            if (ModelState.IsValid)
            {
                vATRegisterBuy.Month = vATRegisterBuy.DateOfIssue.Month;
                vATRegisterBuy.Year = vATRegisterBuy.DateOfIssue.Year;
                vATRegisterBuy.Number = vATRegisterBuy.getOrderNumber (_context);
                _context.Add (vATRegisterBuy);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (vATRegisterBuy);
        }


        // GET: VATRegisterBuys/CreateTest
        public IActionResult CreateTest ()
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            VATRegisterBuyViewModel vATRegisterBuy = new VATRegisterBuyViewModel ();
            DateTime currDate = DateTime.Now;
            vATRegisterBuy.DateOfIssue = currDate;
            vATRegisterBuy.DeliveryDate = currDate;
            vATRegisterBuy.Month = currDate.Month;
            vATRegisterBuy.Year = currDate.Year;

            return View (vATRegisterBuy);
        }

        // POST: VATRegisterBuys/CreateTest
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTest ([Bind ("Id,DeliveryDate,DateOfIssue,DocumentNumber,ContractorId,ValueBrutto,ValueNetto,TaxDeductibleValue,TaxFreeBuysValue,NoTaxDeductibleBuysValue,CarCost,BuyForTrade,OtherCost,DescriptionForTaxBook")] VATRegisterBuyViewModel vATRegisterBuyViewModel)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            if (ModelState.IsValid)
            {
                vATRegisterBuyViewModel.Month = vATRegisterBuyViewModel.DateOfIssue.Month;
                vATRegisterBuyViewModel.Year = vATRegisterBuyViewModel.DateOfIssue.Year;
                vATRegisterBuyViewModel.Number = vATRegisterBuyViewModel.getOrderNumber (_context);
                
                VATRegisterBuy vATRegisterBuy = vATRegisterBuyViewModel.getVATRegisterBuy();
           
                TaxBook taxBook = new TaxBook();
                taxBook.Date = vATRegisterBuyViewModel.DateOfIssue;
                taxBook.InvoiceNumber=vATRegisterBuyViewModel.DocumentNumber;
                taxBook.ContractorId=vATRegisterBuyViewModel.ContractorId;
                taxBook.Description=vATRegisterBuyViewModel.DescriptionForTaxBook;
                
                if(vATRegisterBuyViewModel.BuyForTrade)
                {
                    string description="zakup handlowy";
                    taxBook.GoodsBuys=vATRegisterBuyViewModel.ValueNetto;
                    taxBook.Description=description;
                }
                else if(vATRegisterBuyViewModel.OtherCost)
                {
                    taxBook.OtherCosts=vATRegisterBuyViewModel.ValueNetto; 
                }
                else if (vATRegisterBuyViewModel.CarCost)
                {
                    decimal carCostDeductibleValue = Decimal.Parse(_context.Parameter.Single(p => p.Name == "odl_koszty_pojazd").Value);
                    taxBook.OtherCosts=vATRegisterBuyViewModel.ValueNetto*carCostDeductibleValue;
                    vATRegisterBuy.TaxDeductibleValue = vATRegisterBuy.TaxDeductibleValue / 2;

                    CarCost carCost = new CarCost();
                    carCost.Date = vATRegisterBuyViewModel.DateOfIssue;
                    carCost.Price = vATRegisterBuyViewModel.ValueBrutto;
                    carCost.Description = vATRegisterBuyViewModel.DescriptionForTaxBook;

                    // if (_context.CarCostType.Single(p=>p.Contains(carCost.Description))=!null)
                    // {
                    //     carCost.CarCostTypeId = _context.CarCostType.Single(p=>p.Contains());
                    // }
                }


                _context.Add(taxBook);
                _context.Add (vATRegisterBuy);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (vATRegisterBuyViewModel);
        }





        // GET: VATRegisterBuys/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            if (id == null)
            {
                return NotFound ();
            }

            var vATRegisterBuy = await _context.VATRegisterBuy.SingleOrDefaultAsync (m => m.Id == id);
            if (vATRegisterBuy == null)
            {
                return NotFound ();
            }
            return View (vATRegisterBuy);
        }

        // POST: VATRegisterBuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Number,DeliveryDate,DateOfIssue,DocumentNumber,Contractor,ValueBrutto,ValueNetto,TaxDeductibleValue,TaxFreeBuysValue,NoTaxDeductibleBuysValue")] VATRegisterBuy vATRegisterBuy)
        {
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");

            if (id != vATRegisterBuy.Id)
            {
                return NotFound ();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (vATRegisterBuy);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VATRegisterBuyExists (vATRegisterBuy.Id))
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return View (vATRegisterBuy);
        }

        // GET: VATRegisterBuys/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var vATRegisterBuy = await _context.VATRegisterBuy
                .SingleOrDefaultAsync (m => m.Id == id);
            if (vATRegisterBuy == null)
            {
                return NotFound ();
            }

            return View (vATRegisterBuy);
        }

        // POST: VATRegisterBuys/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var vATRegisterBuy = await _context.VATRegisterBuy.SingleOrDefaultAsync (m => m.Id == id);
            _context.VATRegisterBuy.Remove (vATRegisterBuy);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool VATRegisterBuyExists (int id)
        {
            return _context.VATRegisterBuy.Any (e => e.Id == id);
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

        // GET: GenerateInvoice        
        public async Task<IActionResult> GenerateVATRegister (int? year, int? month)
        {
            if (year == null || month == null)
            {
                return NotFound ();
            }

            var vATRegisterBuy = new VATRegisterBuy ();
            string pdfFilename = vATRegisterBuy.generate (_context, (int) year, (int) month);
            string downFilename = vATRegisterBuy.getDownloadFilename ((int) year, (int) month);
            await Task.Delay (1000);
            return RedirectToAction ("GetPdfFile", new { filename = pdfFilename, downloadFilename = downFilename });
        }

        // GET: GenerateInvoice        
        public async Task<IActionResult> AddToTaxBook (int? year, int? month)
        {
            var VATRegisterBuyItems = _context.VATRegisterBuy.Where(p=>p.Month==month && p.Year==year);
            List<TaxBook> taxBookItems = new List<TaxBook>();


            foreach (VATRegisterBuy v in VATRegisterBuyItems)
            {
                TaxBook taxBook = new TaxBook();
                taxBook.Date = v.DateOfIssue;
                taxBook.InvoiceNumber = v.DocumentNumber;
                taxBook.ContractorId = v.ContractorId;
                //taxBook.Description = v.DescriptionForTaxBook;
                taxBook.OtherCosts = v.ValueNetto;

                taxBookItems.Add(taxBook);
            }

            
            _context.AddRange(taxBookItems);
                await _context.SaveChangesAsync ();
            return null;
        }
    }
}