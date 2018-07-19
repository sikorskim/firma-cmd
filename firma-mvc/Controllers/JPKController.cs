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

        public IActionResult Index()
        {
            ViewData["Month"] = new SelectList(Tools.getMonthsDictionary(), "Key", "Value");
            ViewData["Year"] = new SelectList(Tools.getYearsList());
            ViewData["JPKType"] = new SelectList(Tools.getJPKtypes(), "Key", "Value");
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