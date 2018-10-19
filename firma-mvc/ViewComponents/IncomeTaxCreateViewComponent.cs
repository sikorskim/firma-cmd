using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class IncomeTaxCreateViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public IncomeTaxCreateViewComponent(ApplicationDbContext _context)
        {
            db = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string MyView = "Default";
            ViewData["Month"] = new SelectList(Tools.getMonthsDictionary(), "Key", "Value");
            ViewData["Year"] = new SelectList(Tools.getYearsList());
            return View(MyView, new IncomeTax(DateTime.Now.Year, DateTime.Now.Month-1));
        }
    }
}