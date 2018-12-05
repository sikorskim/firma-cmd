using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using firma_mvc;
using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace firma_mvc.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController (ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index (string searchQuery, int? year, int? month, int? statusId)
        {
            ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", DateTime.Now.Month);
            ViewData["Year"] = new SelectList (Tools.getYearsList (), DateTime.Now.Year);
            ViewData["Status"] = new SelectList (_context.InvoiceStatus.ToList (), "Id", "Name");

            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            ViewData["PaymentMethodId"] = new SelectList (_context.PaymentMethod, "Id", "Name");

            Invoice invoice = new Invoice ();
            invoice.Number = getNumber ();
            invoice.DateOfIssue = DateTime.Now;
            invoice.DateOfDelivery = invoice.DateOfIssue;
            ViewData["Invoice"] = invoice;

            var applicationDbContext = _context.Invoice.Include (i => i.Contractor).Include (i => i.PaymentMethod).Include (i => i.InvoiceItems).Include (i => i.InvoiceStatus);

            if (!String.IsNullOrEmpty (searchQuery))
            {
                var searchResult = applicationDbContext.Where (p => p.Contractor.Name.Contains (searchQuery) || p.Contractor.NIP.Contains (searchQuery) || p.Number.Contains (searchQuery));
                return View (await searchResult.ToListAsync ());
            }

            if (month != null)
            {
                var filteredResult = applicationDbContext.Where (p => p.DateOfIssue.Month == month);
                ViewData["Month"] = new SelectList (Tools.getMonthsDictionary (), "Key", "Value", month);
                if (year != null)
                {
                    filteredResult = applicationDbContext.Where (p => p.DateOfIssue.Month == month && p.DateOfIssue.Year == year);
                    ViewData["Year"] = new SelectList (Tools.getYearsList (), year);
                }
                return View (await filteredResult.ToListAsync ());
            }
            else
            {
                return View (await applicationDbContext.Where (p => p.DateOfIssue.Year == DateTime.Now.Year && p.DateOfIssue.Month == DateTime.Now.Month).ToListAsync ());
                // return View (await applicationDbContext.OrderByDescending(p=>p.DateOfIssue).ToListAsync ());
            }

            //return View(await applicationDbContext.ToListAsync());
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
                //Tools.deleteTempFiles (filename.Substring (4, 64));
                return result;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine (e.Message);
                return NotFound ();
            }
        }

        // GET: GenerateInvoice        
        public async Task<IActionResult> GenerateInvoice (int? id, int typeCode)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var invoice = await _context.Invoice
                .Include (i => i.Contractor)
                .Include (i => i.PaymentMethod)
                .Include (i => i.InvoiceItems)
                .Include (i => i.InvoiceStatus)
                .SingleOrDefaultAsync (m => m.Id == id);

            invoice.InvoiceItems = _context.InvoiceItem.Where (p => p.InvoiceId == id).ToList ();
            foreach (InvoiceItem invoiceItem in invoice.InvoiceItems)
            {
                //Item item = invoiceItem.Item;
                //item.VAT = _context.VAT.Single(p => p.Id == item.VATId);
                //item.UnitOfMeasure = _context.UnitOfMeasure.Single(p => p.Id == item.UnitOfMeasureId);
            }

            if (invoice == null)
            {
                return NotFound ();
            }
            // to change
            invoice.Company = _context.Company.FirstOrDefault ();

            string pdfFilename = invoice.generate (typeCode);
            string downFilename = invoice.getDownloadFilename ();
            await Task.Delay (1000);
            return RedirectToAction ("GetPdfFile", new { filename = pdfFilename, downloadFilename = downFilename });
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var invoice = await _context.Invoice
                .Include (i => i.Contractor)
                .Include (i => i.PaymentMethod)
                .Include (i => i.InvoiceItems)
                .Include (i => i.InvoiceStatus)
                .SingleOrDefaultAsync (m => m.Id == id);

            invoice.InvoiceItems = _context.InvoiceItem.Where (p => p.InvoiceId == id).ToList ();

            if (invoice == null)
            {
                return NotFound ();
            }

            return View (invoice);
        }

        // GET: Invoice/Create
        public IActionResult Create ()
        {
            Invoice invoice = new Invoice ();
            invoice.Number = getNumber ();
            invoice.DateOfIssue = DateTime.Now;
            invoice.DateOfDelivery = invoice.DateOfIssue;
            invoice.Paid = false;

            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name");
            ViewData["PaymentMethodId"] = new SelectList (_context.PaymentMethod, "Id", "Name");
            return View (invoice);
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,Number,DateOfIssue,DateOfDelivery,ContractorId,PaymentMethodId")] Invoice invoice)
        {
            invoice.InvoiceStatusId = _context.InvoiceStatus.Single (p => p.Name == "nowa").Id;
            invoice.Paid = false;
            invoice.CompanyId = getCompanyId ();

            if (ModelState.IsValid)
            {
                _context.Add (invoice);
                await _context.SaveChangesAsync ();
                return RedirectToAction ("Details", "Invoices", new { id = invoice.Id });
            }
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name", invoice.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList (_context.PaymentMethod, "Id", "Name", invoice.PaymentMethodId);
            return View (invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var invoice = await _context.Invoice.SingleOrDefaultAsync (m => m.Id == id);
            if (invoice == null)
            {
                return NotFound ();
            }
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name", invoice.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList (_context.PaymentMethod, "Id", "Name", invoice.PaymentMethodId);
            ViewData["InvoiceStatusId"] = new SelectList (_context.InvoiceStatus, "Id", "Name", invoice.PaymentMethodId);
            return View (invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Number,DateOfIssue,DateOfDelivery,ContractorId,PaymentMethodId,InvoiceStatusId,Paid")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound ();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    invoice.CompanyId = getCompanyId ();
                    _context.Update (invoice);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceHeaderExists (invoice.Id))
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Details), new { id = invoice.Id });
            }
            ViewData["ContractorId"] = new SelectList (_context.Contractor, "Id", "Name", invoice.ContractorId);
            ViewData["PaymentMethodId"] = new SelectList (_context.PaymentMethod, "Id", "Name", invoice.PaymentMethodId);
            ViewData["InvoiceStatusId"] = new SelectList (_context.InvoiceStatus, "Id", "Name", invoice.InvoiceStatusId);
            return View (invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var invoiceHeader = await _context.Invoice
                .Include (i => i.Contractor)
                .Include (i => i.PaymentMethod)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (invoiceHeader == null)
            {
                return NotFound ();
            }

            return View (invoiceHeader);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var invoiceHeader = await _context.Invoice.SingleOrDefaultAsync (m => m.Id == id);
            _context.Invoice.Remove (invoiceHeader);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool InvoiceHeaderExists (int id)
        {
            return _context.Invoice.Any (e => e.Id == id);
        }

        public string getNumber ()
        {
            string number = string.Empty;
            string month = DateTime.Now.Month.ToString ();
            if (month.Length == 1)
            {
                month = month.Insert (0, "0");
            }

            try
            {
                string lastNumber = _context.Invoice.Last (p => p.DateOfIssue.Year == DateTime.Now.Year && p.DateOfIssue.Month == DateTime.Now.Month).Number;
                int nextNumber = Int32.Parse (lastNumber.Substring (lastNumber.LastIndexOf ('/') + 1, lastNumber.Length - lastNumber.LastIndexOf ('/') - 1));
                nextNumber++;

                number = "FV/" + DateTime.Now.Year + "/" + month + "/" + nextNumber.ToString ();
            }
            catch (Exception)
            {
                number = "FV/" + DateTime.Now.Year + "/" + month + "/1";
            }

            return number;
        }

        int countItems (int invoiceId)
        {
            try
            {
                int count = _context.InvoiceItem.Where (p => p.InvoiceId == invoiceId).Count ();
                Console.WriteLine (count);
                return count;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        int getCompanyId ()
        {
            return _context.Company.FirstOrDefault ().Id;
        }

        // GET: Invoice/Confirm
        public IActionResult Confirm (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            Invoice invoice = _context.Invoice.Include (i => i.InvoiceItems).Include (i => i.InvoiceStatus).Single (m => m.Id == id);

            if (invoice.InvoiceStatus.Name != "zatwierdzona")
            {
                invoice.InvoiceStatusId = _context.InvoiceStatus.Single (p => p.Name == "zatwierdzona").Id;
                VATRegisterSell sell = new VATRegisterSell ();
                TaxBook taxBook = new TaxBook ();

                try
                {
                    sell = _context.VATRegisterSell.Single (p => p.DocumentNumber == invoice.Number);
                    sell.Number = sell.getOrderNumber (_context);
                    sell.DeliveryDate = invoice.DateOfDelivery;
                    sell.DateOfIssue = invoice.DateOfIssue;
                    sell.Month = sell.DateOfIssue.Month;
                    sell.Year = sell.DateOfIssue.Year;
                    sell.DocumentNumber = invoice.Number;
                    sell.ContractorId = invoice.ContractorId;
                    sell.ValueBrutto = invoice.TotalValueInclVat;

                    sell.ValueNetto23 = 0;
                    sell.VATValue23 = 0;
                    // TO DO: add other VAT rates support
                    foreach (InvoiceItem item in invoice.InvoiceItems)
                    {
                        if (item.VATValue == 23)
                        {
                            sell.ValueNetto23 += item.TotalPrice;
                            sell.VATValue23 += item.TotalVATValue;
                        }
                    }

                    _context.Update (sell);
                }
                catch (Exception)
                {

                    sell.Number = sell.getOrderNumber (_context);
                    sell.DeliveryDate = invoice.DateOfDelivery;
                    sell.DateOfIssue = invoice.DateOfIssue;
                    sell.Month = sell.DateOfIssue.Month;
                    sell.Year = sell.DateOfIssue.Year;
                    sell.DocumentNumber = invoice.Number;
                    sell.ContractorId = invoice.ContractorId;
                    sell.ValueBrutto = invoice.TotalValueInclVat;

                    // TO DO: add other VAT rates support
                    foreach (InvoiceItem item in invoice.InvoiceItems)
                    {
                        if (item.VATValue == 23)
                        {
                            sell.ValueNetto23 += item.TotalPrice;
                            sell.VATValue23 += item.TotalVATValue;
                        }
                    }
                    _context.Add (sell);
                }

                try
                {
                    taxBook=_context.TaxBookItem.Single(p=>p.InvoiceNumber==invoice.Number);
                    taxBook.Date = invoice.DateOfIssue;
                    taxBook.ContractorId = invoice.ContractorId;
                    taxBook.SellValue = invoice.TotalValue;
                    taxBook.Description = "sprzedaż";
                    _context.Update(taxBook);
                }
                catch (Exception)
                {
                    taxBook.Number = taxBook.getOrderNumber (_context);
                    taxBook.Date = invoice.DateOfIssue;
                    taxBook.InvoiceNumber = invoice.Number;
                    taxBook.ContractorId = invoice.ContractorId;
                    taxBook.SellValue = invoice.TotalValue;
                    taxBook.Description = "sprzedaż";
                    _context.Add (taxBook);
                }

                _context.Update (invoice);
                _context.SaveChanges ();
                return RedirectToAction (nameof (Index));
            }
            else
            {
                invoice.InvoiceStatusId = _context.InvoiceStatus.Single (p => p.Name == "nowa").Id;
                _context.Update (invoice);
                _context.SaveChanges ();
                return RedirectToAction (nameof (Details), new { id = id });
            }
        }

        public async Task<IActionResult> SetPaid (int? invoiceId)
        {
            if (invoiceId == null)
            {
                return NotFound ();
            }

            var invoice = _context.Invoice.Single (p => p.Id == invoiceId);
            invoice.Paid = !invoice.Paid;
            _context.Update (invoice);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Details), new { id = invoiceId });
        }
    }
}