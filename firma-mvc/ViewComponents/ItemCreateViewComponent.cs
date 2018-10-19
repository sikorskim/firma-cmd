using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class ItemCreateViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public ItemCreateViewComponent(ApplicationDbContext _context)
        {
            db = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string MyView = "Default";
            ViewData["UnitOfMeasureId"] = new SelectList(db.UnitOfMeasure, "Id", "ShortName");
            ViewData["VATId"] = new SelectList(db.VAT, "Id", "Value");
            return View(MyView, new Item());
        }
    }
}