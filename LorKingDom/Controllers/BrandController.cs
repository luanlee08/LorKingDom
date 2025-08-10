using LorKingDom.Models;
using LorKingDom.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LorKingDom.Controllers
{
    public class BrandController : Controller
    {
        private readonly LorKingDomContext _context;
        public BrandController(LorKingDomContext context) => _context = context;

        // GET: /Brand
        public async Task<IActionResult> Index()
        {
            var list = await _context.Brands
                .OrderByDescending(b => b.BrandId)
                .Select(b => new BrandViewModel
                {
                    BrandId = b.BrandId,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt
                }).ToListAsync();

            return View(list);
        }

        // POST: /Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["CreateErrors"] = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return RedirectToAction(nameof(Index));
            }

            var entity = new Brand
            {
                Name = vm.Name,
                // CreatedAt có default GETDATE() trong DB, không cần set cũng được
            };

            _context.Brands.Add(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Brand/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandViewModel vm)
        {
            if (id != vm.BrandId) return NotFound();
            if (!ModelState.IsValid)
            {
                TempData["EditErrors"] = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return RedirectToAction(nameof(Index));
            }

            var entity = await _context.Brands.FindAsync(id);
            if (entity == null) return NotFound();

            entity.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Brand/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Brands.FindAsync(id);
            if (entity != null)
            {
                _context.Brands.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Không dùng view riêng
        //[HttpGet]
        //public IActionResult Create() => RedirectToAction(nameof(Index));
        //[HttpGet]
        //public IActionResult Edit(int id) => RedirectToAction(nameof(Index));
        //[HttpGet]
        //public IActionResult Delete(int id) => RedirectToAction(nameof(Index));
    }
}
