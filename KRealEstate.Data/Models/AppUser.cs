using Microsoft.AspNetCore.Identity;

namespace KRealEstate.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {

        //public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public bool Organization { get; set; }
        public string TaxId { get; set; } = null!;
        public string AddressId { get; set; } = null!;

        public virtual Ward Address { get; set; } = null!;

    }
}
