using Microsoft.AspNetCore.Identity;

namespace KRealEstate.Data.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = null!;
    }
}
