using LorKingDom.Models;
using LorKingDom.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LorKingDom.Controllers
{
    public class AgeController : Controller
    {
        private readonly LorKingDomContext _context;
        public AgeController(LorKingDomContext context) => _context = context;

        // Chỉ đọc: liệt kê từ DB
        public async Task<IActionResult> Index()
        {
            var list = await _context.Ages
                .OrderBy(a => a.AgeId)
                .Select(a => new AgeViewModel
                {
                    AgeId = a.AgeId,
                    AgeRange = a.AgeRange,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return View(list);
        }

        // (Không hỗ trợ Create/Edit/Delete)
    }
}
