using LorKingDom.Models;
using LorKingDom.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LorKingDom.Controllers
{
    public class ProductController : Controller
    {
        private readonly LorKingDomContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(LorKingDomContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? q)
        {
            await LoadSelectLists();

            var products = await _context.Products
                .Where(p => !p.IsDeleted && (string.IsNullOrEmpty(q) || p.Name.Contains(q)))
                .Select(p => new ProductListItemViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    CategoryName = p.Category != null ? p.Category.Name : "",
                    BrandName = p.Brand != null ? p.Brand.Name : "",
                    Price = p.Price,
                    Quantity = p.Quantity,
                    MainImage = p.ProductImages.Where(i => i.IsMain).Select(i => i.Image).FirstOrDefault()
                                   ?? p.ProductImages.Select(i => i.Image).FirstOrDefault(),
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId,
                    PriceRangeId = p.PriceRangeId,
                    SexId = p.SexId,
                    AgeId = p.AgeId,
                    MaterialId = p.MaterialId,
                    OriginId = p.OriginId,
                    Description = p.Description
                })
                .OrderByDescending(p => p.ProductId)
                .ToListAsync();

            ViewBag.Q = q;
            return View(products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction(nameof(Index), new { q = (string?)null });
            }

            var product = new Product
            {
                Name = vm.Name,
                CategoryId = vm.CategoryId,
                SexId = vm.SexId,
                AgeId = vm.AgeId,
                MaterialId = vm.MaterialId,
                OriginId = vm.OriginId,
                BrandId = vm.BrandId,
                PriceRangeId = vm.PriceRangeId,
                Price = vm.Price,
                Quantity = vm.Quantity,
                Description = vm.Description,
                Status = "Available",
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // ảnh
            var folder = Path.Combine(_env.WebRootPath, "uploads", "products", product.ProductId.ToString());
            Directory.CreateDirectory(folder);

            if (vm.MainImage is { Length: > 0 })
            {
                var mainName = "main_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                               + Path.GetExtension(vm.MainImage.FileName);
                var mainPath = Path.Combine(folder, mainName);
                using var fs = System.IO.File.Create(mainPath);
                await vm.MainImage.CopyToAsync(fs);

                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = product.ProductId,
                    Image = $"/uploads/products/{product.ProductId}/{mainName}",
                    IsMain = true
                });
            }

            if (vm.DetailImages != null && vm.DetailImages.Count > 0)
            {
                int count = 0;
                foreach (var f in vm.DetailImages.Where(x => x != null && x.Length > 0))
                {
                    if (++count > 8) break;
                    var name = "detail_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                               + Path.GetExtension(f.FileName);
                    var path = Path.Combine(folder, name);
                    using var dfs = System.IO.File.Create(path);
                    await f.CopyToAsync(dfs);

                    _context.ProductImages.Add(new ProductImage
                    {
                        ProductId = product.ProductId,
                        Image = $"/uploads/products/{product.ProductId}/{name}",
                        IsMain = false
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã thêm sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel vm)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null || p.IsDeleted)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            p.Name = vm.Name;
            p.CategoryId = vm.CategoryId;
            p.SexId = vm.SexId;
            p.AgeId = vm.AgeId;
            p.MaterialId = vm.MaterialId;
            p.OriginId = vm.OriginId;
            p.BrandId = vm.BrandId;
            p.PriceRangeId = vm.PriceRangeId;
            p.Price = vm.Price;
            p.Quantity = vm.Quantity;
            p.Description = vm.Description;
            p.UpdatedAt = DateTime.Now;

            // ảnh main mới (tuỳ chọn)
            var folder = Path.Combine(_env.WebRootPath, "uploads", "products", p.ProductId.ToString());
            Directory.CreateDirectory(folder);

            if (vm.MainImage is { Length: > 0 })
            {
                var mainName = "main_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                               + Path.GetExtension(vm.MainImage.FileName);
                var mainPath = Path.Combine(folder, mainName);
                using var fs = System.IO.File.Create(mainPath);
                await vm.MainImage.CopyToAsync(fs);

                // unset main cũ
                var oldMains = _context.ProductImages.Where(pi => pi.ProductId == p.ProductId && pi.IsMain);
                foreach (var m in oldMains) m.IsMain = false;

                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = p.ProductId,
                    Image = $"/uploads/products/{p.ProductId}/{mainName}",
                    IsMain = true
                });
            }

            // detail mới (append)
            if (vm.DetailImages != null && vm.DetailImages.Count > 0)
            {
                int count = 0;
                foreach (var f in vm.DetailImages.Where(x => x != null && x.Length > 0))
                {
                    if (++count > 8) break;
                    var name = "detail_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                               + Path.GetExtension(f.FileName);
                    var path = Path.Combine(folder, name);
                    using var dfs = System.IO.File.Create(path);
                    await f.CopyToAsync(dfs);

                    _context.ProductImages.Add(new ProductImage
                    {
                        ProductId = p.ProductId,
                        Image = $"/uploads/products/{p.ProductId}/{name}",
                        IsMain = false
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p != null)
            {
                p.IsDeleted = true;
                p.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xoá sản phẩm.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSelectLists()
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Sexes = await _context.Sexes.ToListAsync();
            ViewBag.Ages = await _context.Ages.ToListAsync();
            ViewBag.Materials = await _context.Materials.Where(m => !m.IsDeleted).ToListAsync();
            ViewBag.Origins = await _context.Origins.Where(o => !o.IsDeleted).ToListAsync();
            ViewBag.Brands = await _context.Brands.ToListAsync();
            ViewBag.PriceRanges = await _context.PriceRanges.ToListAsync();
        }
    }
}
