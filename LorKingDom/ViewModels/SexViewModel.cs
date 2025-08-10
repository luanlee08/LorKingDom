using System;
using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class SexViewModel
    {
        public int SexId { get; set; }

        [Display(Name = "Giới tính")]
        public string Name { get; set; } = null!;

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }
    }
}
