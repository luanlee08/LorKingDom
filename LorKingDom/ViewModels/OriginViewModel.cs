using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class OriginViewModel
    {
        public int OriginId { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập xuất xứ")]
        [StringLength(20, ErrorMessage = "Xuất xứ không được vượt quá 100 ký tự")]
        [RegularExpression(@"^[\p{L}0-9\s\-]+$", ErrorMessage = "Xuất xứ chỉ được chứa chữ, số, dấu cách và dấu '-'")]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
