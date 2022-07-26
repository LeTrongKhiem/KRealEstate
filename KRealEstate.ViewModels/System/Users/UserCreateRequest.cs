using System.ComponentModel.DataAnnotations;

namespace KRealEstate.ViewModels.System.Users
{
    public class UserCreateRequest
    {
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }
        [Display(Name = "Bạn là")]
        public bool Organization { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [Display(Name = "Địa chỉ")]
        public string AddressId { get; set; }
        public string ProviceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public int RegionId { get; set; }
        public int UnitId { get; set; }
        [Display(Name = "Mã số thuế/CMND")]
        public string TaxId { get; set; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        public string ComfirmPassword { get; set; }
        public bool CheckRobot { get; set; }
    }
}
