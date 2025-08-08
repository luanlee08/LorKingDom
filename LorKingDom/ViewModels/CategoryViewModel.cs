using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public int SuperCategoryId { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? SuperCategoryName { get; set; }
    }
}
