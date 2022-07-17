using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class Post
    {
        public string Id { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime DatePost { get; set; }
        public bool Status { get; set; }

        public virtual PostDetail PostNavigation { get; set; } = null!;
    }
}
