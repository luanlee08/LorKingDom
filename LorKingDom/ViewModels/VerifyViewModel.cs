using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class VerifyViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }
    }
}