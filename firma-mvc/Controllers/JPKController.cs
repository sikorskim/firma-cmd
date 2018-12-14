using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firma_mvc.Controllers
{
    public class JPKController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JPKController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string info)
        {
            if (!string.IsNullOrEmpty(info))
            {
                ViewBag.Info = info;
            }
            ViewData["Month"] = new SelectList(Tools.getMonthsDictionary(), "Key", "Value");
            ViewData["Year"] = new SelectList(Tools.getYearsList());
            ViewData["JPKType"] = new SelectList(Tools.getJPKtypes(), "Key", "Value");
            JPKViewModel jpkViewModel = new JPKViewModel();
            jpkViewModel.Month = DateTime.Now.Month - 1; 
            jpkViewModel.Year = DateTime.Now.Year;
            return View(jpkViewModel);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("JPKTypeId,Year,Month")] JPKViewModel jpkViewModel)
        {
                if (jpkViewModel.JPKTypeId == 1)
                {
                    JPK_VAT jpk_vat = new JPK_VAT(jpkViewModel.Month, jpkViewModel.Year, _context);
                    string xmlFilename = jpk_vat.generate();
                if (jpk_vat.SprzedazCtrl.LiczbaWierszySprzedazy == 0 && jpk_vat.ZakupCtrl.LiczbaWierszyZakupow == 0)
                {
                    return RedirectToAction(nameof(Index), new { info = "Brak danych dla wybranego okresu" });
                }
                    //await Task.Delay(1000);
                    return RedirectToAction("GetXmlFile", new { filename = xmlFilename });
                }

            ViewData["Month"] = new SelectList(Tools.getMonthsDictionary(), "Key", "Value");
            ViewData["Year"] = new SelectList(Tools.getYearsList());
            ViewData["JPKType"] = new SelectList(Tools.getJPKtypes(), "Key", "Value");

            return View(jpkViewModel);
        }

        public IActionResult GetXmlFile(string filename)
        {
            const string contentType = "application/xml";
            HttpContext.Response.ContentType = contentType;
            FileContentResult result = null;
            filename = "tmp/" + filename;

            try
            {
                result = new FileContentResult(System.IO.File.ReadAllBytes(filename), contentType)
                {
                    FileDownloadName = $"out.xml"
                };
                Tools.deleteTempFiles(filename.Substring(4, 64));
                return result;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
    }
}