using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class PostDetail
    {
        public PostDetail()
        {
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int DayLengthPost { get; set; }
        public DateTime? DayPostStart { get; set; }
        public DateTime? DayPostEnd { get; set; }
        public string? PostTypeId { get; set; }

        public virtual PostType? PostType { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }
    }
}
