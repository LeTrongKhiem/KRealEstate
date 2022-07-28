using FluentValidation;

namespace KRealEstate.ViewModels.System.Users
{
    public class CreateUserValidation : AbstractValidator<UserCreateRequest>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username là bắt buộc")
                                    .MaximumLength(50).WithMessage("Username tối đa 50 ký tự")
                                    .MinimumLength(6).WithMessage("Username tối thiểu 6 ký tự");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Họ không thể để trống");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Tên không thể để trống");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!!").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email is not format");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required!!").Matches(@"(84|0[3|5|7|8|9])+([0-9]{8})\b").WithMessage("Phone number is not format");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot Greater Than 100 year!!!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Phone is required!!")//.Matches(@"?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\s])^.{10,}$").WithMessage("Mật khẩu phải có ít nhất 1 chữ in hoa, ký tự đặc biệt")
                .MinimumLength(6).WithMessage("Mật khẩu phải trên 6 ký tự");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (!request.Password.Equals(request.ComfirmPassword))
                {
                    context.AddFailure("Mật khẩu xác nhận không chính xác!");
                }
            });
            RuleFor(x => x.ProviceCode).NotEmpty().WithErrorCode("Tỉnh thành không được phép trống");
            RuleFor(x => x.DistrictCode).NotEmpty().WithErrorCode("Quận/huyện không được phép trống");
            RuleFor(x => x.WardCode).NotEmpty().WithErrorCode("Khu vực không được phép trống");
        }
    }
}
