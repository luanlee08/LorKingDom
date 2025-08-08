using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên chất liệu")]
        [StringLength(100, ErrorMessage = "Tên chất liệu không được vượt quá 100 ký tự")]
        public string Name { get; set; } = null!;
        
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
