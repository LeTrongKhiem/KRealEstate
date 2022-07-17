using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class PostType
    {
        public PostType()
        {
            PostDetails = new HashSet<PostDetail>();
            UnitPricePosts = new HashSet<UnitPricePost>();
        }

        public string Id { get; set; } = null!;
        public string NamePostType { get; set; } = null!;

        public virtual ICollection<PostDetail> PostDetails { get; set; }
        public virtual ICollection<UnitPricePost> UnitPricePosts { get; set; }
    }
}
