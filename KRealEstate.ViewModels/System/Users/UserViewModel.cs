using System.ComponentModel.DataAnnotations;

namespace KRealEstate.ViewModels.System.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        public string AddressId { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
