using LorKingDom.Models;
using LorKingDom.ViewModels;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LorKingDom.Controllers
{
    public class SexController : Controller
    {
        private readonly LorKingDomContext _context;
        public SexController(LorKingDomContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.Sexes
                .OrderBy(s => s.SexId)
                .Select(s => new SexViewModel
                {
                    SexId = s.SexId,
                    Name = s.Name,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();

            return View(list);
        }

    }
}
