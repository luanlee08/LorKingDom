using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public int SuperCategoryId { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập danh mục")]
        [StringLength(100, ErrorMessage = "Danh mục không được vượt quá 100 ký tự")]
        [RegularExpression(@"^[\p{L}0-9\s\-]+$", ErrorMessage = "Danh mục chỉ được chứa chữ, số, dấu cách và dấu '-'")]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? SuperCategoryName { get; set; }
    }
}
