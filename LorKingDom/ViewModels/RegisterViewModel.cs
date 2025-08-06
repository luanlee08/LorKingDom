using System.ComponentModel.DataAnnotations;

namespace LorKingDom.ViewModels
{
    public class RegisterViewModel
    {
        [Required]            public string UserName      { get; set; }
        [Required][EmailAddress] public string Email     { get; set; }
        [Required][Phone]     public string PhoneNumber   { get; set; }   // <— đổi tên
        [Required][DataType(DataType.Password)]
        public string Password    { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }

}