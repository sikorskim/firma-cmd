using firma_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class ContractorCreateViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public ContractorCreateViewComponent(ApplicationDbContext _context)
        {
            db = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string MyView = "Default";
            return View(MyView, new Contractor());
        }
    }
}