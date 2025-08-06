using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LorKingDom.Models;

namespace LorKingDom.Controllers
{
    public class BrandController : Controller
    {
        private readonly LorKingDomContext _context;

        public BrandController(LorKingDomContext context)
        {
            _context = context;
        }

        // GET: /Brand
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new BrandViewModel
            {
                Brands = await _context.Brands.ToListAsync(),
                NewBrand = new Brand(),
                EditedBrand = new Brand()
            };
            return View("~/Views/Admin/Brand.cshtml", vm);
        }

        // GET: /Brand/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // POST: /Brand/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Brand brand)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Brand/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,Name")] Brand brand)
        {
            if (id != brand.BrandId)
                return BadRequest();

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            try
            {
                _context.Brands.Update(brand);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(brand.BrandId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Brand/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _context.Brands.FindAsync(id);
            if (toDelete != null)
            {
                _context.Brands.Remove(toDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
            => _context.Brands.Any(e => e.BrandId == id);
    }
}