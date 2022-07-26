using System.ComponentModel.DataAnnotations;

namespace KRealEstate.ViewModels.System.Users
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Password phải giống nhau.")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}
