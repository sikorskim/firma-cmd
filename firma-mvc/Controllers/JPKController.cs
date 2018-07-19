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

        Dictionary<int, string> months = new Dictionary<int, string>()
            {
                { 1, "styczeń" },
                { 2, "luty" },
                { 3, "marzec"},
                {4, "kwiecień" },
                {5, "maj" },
                {6,"czerwiec" },
                {7,"lipiec" },
                {8,"sierpień" },
                {9,"wrzesień" },
                {10, "październik" },
                {11, "listopad" },
                {12, "grudzień" }
            };

        List<int> years = new List<int>()
            { 2018 };

        Dictionary<int, string> jpkType = new Dictionary<int, string>()
            {
                {1,"JPK_VAT" },
            {2, "JPK Księga" }
            };

        public IActionResult Index()
        {
            ViewData["Month"] = new SelectList(months, "Key", "Value");
            ViewData["Year"] = new SelectList(years);
            ViewData["JPKType"] = new SelectList(jpkType, "Key", "Value");
            return View();
        }

        // GET: JPK/Generate
        public async Task<IActionResult> Generate(int jpkTypeCode, int month, int year)
        {
            if (jpkTypeCode == 1)
            {
                JPK_VAT jpk_vat = new JPK_VAT(month, year, _context);
                string xmlFilename = jpk_vat.generate();
                //await Task.Delay(1000);
                return RedirectToAction("GetXmlFile", new { filename = xmlFilename });
            }

            ViewData["Month"] = new SelectList(months, "Key", "Value");
            ViewData["Year"] = new SelectList(years);
            ViewData["JPKType"] = new SelectList(jpkType, "Key", "Value");
            return View();
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