using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class GeneralSettingsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public GeneralSettingsViewComponent(ApplicationDbContext _context)
        {
            db = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string MyView = "Default";
            ViewData["PaymentMethods"] = await db.PaymentMethod.ToListAsync();
            ViewData["UnitsOfMeasure"] = await db.UnitOfMeasure.ToListAsync();
            ViewData["InvoiceStatuses"]  =await db.InvoiceStatus.ToListAsync();
            ViewData["VATrates"] = await db.VAT.ToListAsync();

            return View(MyView);
        }
    }
}