using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Requests = new HashSet<Request>();
            Reviews = new HashSet<Review>();
        }

        public int ParentId { get; set; }
        public int UserId { get; set; }
        public int ChildrenCount { get; set; }
        public int ChildrenMinAge { get; set; }
        public int ChildrenMaxAge { get; set; }
        public bool HasDog { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
