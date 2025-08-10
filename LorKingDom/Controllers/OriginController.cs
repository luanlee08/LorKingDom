using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LorKingDom.Models;
using LorKingDom.ViewModels;

namespace LorKingDom.Controllers
{
    public class OriginController : Controller
    {
        private readonly LorKingDomContext _context;

        public OriginController(LorKingDomContext context)
        {
            _context = context;
        }

        // GET: Origin
        public async Task<IActionResult> Index()
        {
            var origins = await _context.Origins
                .Select(o => new OriginViewModel
                {
                    OriginId = o.OriginId,
                    Name = o.Name,
                    IsDeleted = o.IsDeleted,
                    CreatedAt = o.CreatedAt
                }).ToListAsync();

            return View(origins);
        }

        // GET: Origin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Origin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OriginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var origin = new Origin
                {
                    Name = vm.Name,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now
                };

                _context.Add(origin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Origin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var origin = await _context.Origins.FindAsync(id);
            if (origin == null) return NotFound();

            var vm = new OriginViewModel
            {
                OriginId = origin.OriginId,
                Name = origin.Name,
                IsDeleted = origin.IsDeleted,
                CreatedAt = origin.CreatedAt
            };

            return View(vm);
        }

        // POST: Origin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OriginViewModel vm)
        {
            if (id != vm.OriginId) return NotFound();

            if (ModelState.IsValid)
            {
                var origin = await _context.Origins.FindAsync(id);
                if (origin == null) return NotFound();

                origin.Name = vm.Name;
                origin.IsDeleted = vm.IsDeleted;

                _context.Update(origin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Origin/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var origin = await _context.Origins.FindAsync(id);
            if (origin == null) return NotFound();

            var vm = new OriginViewModel
            {
                OriginId = origin.OriginId,
                Name = origin.Name,
                CreatedAt = origin.CreatedAt,
                IsDeleted = origin.IsDeleted
            };

            return View(vm);
        }

        // POST: Origin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var origin = await _context.Origins.FindAsync(id);
            if (origin != null)
            {
                _context.Origins.Remove(origin);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
