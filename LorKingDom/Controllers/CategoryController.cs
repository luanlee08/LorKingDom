using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LorKingDom.Models;
using LorKingDom.ViewModels;

namespace LorKingDom.Controllers
{
    public class CategoryController : Controller
    {
        private readonly LorKingDomContext _context;

        public CategoryController(LorKingDomContext context)
        {
            _context = context;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {
            ViewBag.SuperCategories = await _context.SuperCategories.ToListAsync();

            var categories = await _context.Categories
                .Include(c => c.SuperCategory)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    SuperCategoryId = c.SuperCategoryId,
                    SuperCategoryName = c.SuperCategory.Name,
                    IsDeleted = c.IsDeleted,
                    CreatedAt = c.CreatedAt
                }).ToListAsync();

            return View(categories);
        }

        // GET: /Category/Create
        public IActionResult Create()
        {
            ViewBag.SuperCategories = _context.SuperCategories.ToList();
            return View();
        }

        // POST: /Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SuperCategories = _context.SuperCategories.ToList();
            return View(category);
        }

        // GET: /Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            ViewBag.SuperCategories = _context.SuperCategories.ToList();
            return View(category);
        }

        // POST: /Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SuperCategories = _context.SuperCategories.ToList();
            return View(category);
        }

        // GET: /Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
