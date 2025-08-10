using LorKingDom.Models;
using LorKingDom.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LorKingDom.Controllers
{
    public class PriceRangeController : Controller
    {
        private readonly LorKingDomContext _context;
        public PriceRangeController(LorKingDomContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.PriceRanges
                .OrderBy(p => p.PriceRangeId)
                .Select(p => new PriceRangeViewModel
                {
                    PriceRangeId = p.PriceRangeId,
                    PriceRange = p.PriceRange1,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            return View(list);
        }

    }
}
