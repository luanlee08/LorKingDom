using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên thương hiệu")]
        [StringLength(100, ErrorMessage = "Tên thương hiệu không được vượt quá 100 ký tự")]
        [RegularExpression(@"^[\p{L}0-9\s\-]+$", ErrorMessage = "Tên thương hiệu chỉ được chứa chữ, số, dấu cách và dấu '-'")]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Products { get; set; } = new List<string>(); // Danh sách sản phẩm liên kết với thương hiệu
    }
}
