using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int ParentId { get; set; }
        public int BabySitterId { get; set; }
        public int Rating { get; set; }
        public string Decription { get; set; }

        public virtual BabySitter BabySitter { get; set; }
        public virtual Parent Parent { get; set; }
    }
}
