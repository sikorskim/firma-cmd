using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class AddInvoiceItemViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public AddInvoiceItemViewComponent(ApplicationDbContext _context)
        {
            db = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(InvoiceItem invoiceItem)
        {
            string MyView = "Default";
            ViewData["ItemId"] = new SelectList(db.Item, "Id", "Name");
            return View(MyView, invoiceItem);
        }
    }
}