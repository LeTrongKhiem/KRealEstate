using FluentValidation;

namespace KRealEstate.ViewModels.System.Users
{
    public class LoginValidation : AbstractValidator<LoginRequest>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Tài khoản không thể để trống");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không thể để trống")
                .MinimumLength(6).WithMessage("Mật khẩu tối thiểu 6 ký tự");

        }
    }
}
