using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class OriginViewModel
    {
        public int OriginId { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên xuất xứ")]
        [StringLength(20, ErrorMessage = "Tên xuất xứ không được vượt quá 100 ký tự")]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
