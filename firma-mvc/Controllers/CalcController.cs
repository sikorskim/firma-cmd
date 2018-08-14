using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firma_mvc.Controllers
{
    public class CalcController : Controller
    {

        Dictionary<int, string> CalcTypes = new Dictionary<int, string> ()
        { { 0, "netto-brutto" }, { 1, "brutto-netto" }
        };
        public IActionResult Index (decimal? nettoVal, decimal? bruttoVal)
        {
            decimal netto = 0;
            decimal brutto = 0;
            if (nettoVal != null && bruttoVal != null)
            {
                netto = (decimal) nettoVal;
                brutto = (decimal) bruttoVal;
            }

            ViewData["CalcType"] = new SelectList (CalcTypes, "Key", "Value");
            ViewData["NettoVal"] = netto.ToString ("c");
            ViewData["BruttoVal"] = brutto.ToString ("c");
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index ([Bind ("NettoValue,BruttoValue,CalcTypeId")] CalcViewModel calcViewModel)
        {
            if (calcViewModel.CalcTypeId == 0)
            {
                calcViewModel.BruttoValue = calcViewModel.NettoValue * (decimal) 1.23;
                calcViewModel.BruttoValue = Tools.decimalRound (calcViewModel.BruttoValue);
            }
            else if (calcViewModel.CalcTypeId == 1)
            {
                calcViewModel.NettoValue = calcViewModel.BruttoValue * 100 / 123;
                calcViewModel.NettoValue = Tools.decimalRound (calcViewModel.NettoValue);
            }
            ViewData["CalcType"] = new SelectList (CalcTypes, "Key", "Value");
            return RedirectToAction ("Index", new { nettoVal = calcViewModel.NettoValue, bruttoVal = calcViewModel.BruttoValue });
        }
    }
}