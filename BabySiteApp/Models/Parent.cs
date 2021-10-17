using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Requests = new List<Request>();
            Reviews = new List<Review>();
        }

        public int ParentId { get; set; }
        public int UserId { get; set; }
        public int ChildrenCount { get; set; }
        public int ChildrenMinAge { get; set; }
        public int ChildrenMaxAge { get; set; }
        public bool HasDog { get; set; }

        public virtual User User { get; set; }
        public virtual List<Request> Requests { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
