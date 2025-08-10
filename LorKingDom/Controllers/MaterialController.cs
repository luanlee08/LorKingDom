using LorKingDom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LorKingDom.ViewModels;

namespace LorKingDom.Controllers
{
    public class MaterialController : Controller
    {
        private readonly LorKingDomContext _context;

        public MaterialController(LorKingDomContext context)
        {
            _context = context;
        }

        // GET: Material
        public async Task<IActionResult> Index()
        {
            var list = await _context.Materials
                .Select(m => new MaterialViewModel
                {
                    MaterialId = m.MaterialId,
                    Name = m.Name,
                    Description = m.Description,
                    IsDeleted = m.IsDeleted
                }).ToListAsync();

            return View(list);
        }

        // GET: Material/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var material = new Material
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    IsDeleted = false
                };
                _context.Materials.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Material/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return NotFound();

            var vm = new MaterialViewModel
            {
                MaterialId = material.MaterialId,
                Name = material.Name,
                Description = material.Description,
                IsDeleted = material.IsDeleted
            };

            return View(vm);
        }

        // POST: Material/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaterialViewModel vm)
        {
            if (id != vm.MaterialId) return NotFound();

            if (ModelState.IsValid)
            {
                var material = await _context.Materials.FindAsync(id);
                if (material == null) return NotFound();

                material.Name = vm.Name;
                material.Description = vm.Description;
                material.IsDeleted = vm.IsDeleted;

                _context.Materials.Update(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Material/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return NotFound();

            var vm = new MaterialViewModel
            {
                MaterialId = material.MaterialId,
                Name = material.Name,
                Description = material.Description,
                IsDeleted = material.IsDeleted
            };

            return View(vm);
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
