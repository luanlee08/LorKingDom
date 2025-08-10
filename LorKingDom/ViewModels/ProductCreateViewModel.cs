using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LorKingDom.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required, StringLength(255)]
        
        public string Name { get; set; } = null!;

        [Required] public int CategoryId { get; set; }
        [Required] public int SexId { get; set; }
        [Required] public int AgeId { get; set; }
        [Required] public int MaterialId { get; set; }
        [Required] public int OriginId { get; set; }
        [Required] public int BrandId { get; set; }
        [Required] public int PriceRangeId { get; set; }

        [Required, Range(0, 999999999)]
        public decimal Price { get; set; }

        [Required, Range(0, 10000000)]
        public int Quantity { get; set; }         

        public string? Description { get; set; }

        // Ảnh
        public IFormFile? MainImage { get; set; }
        public List<IFormFile>? DetailImages { get; set; }
    }
}
