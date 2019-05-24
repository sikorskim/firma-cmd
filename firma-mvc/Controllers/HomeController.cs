using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using firma_mvc.Models;
using firma_mvc.Data;

namespace firma_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int currMonth = DateTime.Now.Month;
            int currYear = DateTime.Now.Year;
            VATRegisterSell vATRegisterSell = new VATRegisterSell(_context);
            VATRegisterBuy vATRegisterBuy = new VATRegisterBuy(_context);
            IncomeTax incomeTax=new IncomeTax(currYear,currMonth);
            VAT7 vat7 = new VAT7(currYear,currMonth);

            ViewData["SellNettValue"]= vATRegisterSell.getNettSellValueByMonth(currMonth,currYear).ToString ("0.00");
            ViewData["SellWithTaxValue"]= vATRegisterSell.getSellWithTaxValueByMonth(currMonth,currYear).ToString ("0.00");
            ViewData["BuyNettValue"]= vATRegisterBuy.getNettBuyValueByMonth(currMonth,currYear).ToString ("0.00");
            ViewData["BuyWithTaxValue"]= vATRegisterBuy.getBuyWithTaxValueByMonth(currMonth,currYear).ToString ("0.00");
            ViewData["IncomeTaxValue"] = incomeTax.compute(_context).Value;
            ViewData["VATValue"] = vat7.compute(_context);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Settings(string info)
        {
            if (!string.IsNullOrEmpty(info))
            {
                ViewBag.Info = info;
            }
            
            ViewData["Company"] = _context.Company.FirstOrDefault();
            ViewData["Parameters"] = _context.Parameter.ToList();
            ViewData["PaymentMethods"] = _context.PaymentMethod.ToList();
            ViewData["UnitsOfMeasure"] = _context.UnitOfMeasure.ToList();
            ViewData["InvoiceStatuses"] = _context.InvoiceStatus.ToList();
            ViewData["VATrates"] = _context.VAT.ToList();

            return View();
        }
    }
}
