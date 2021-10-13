using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class BabySitter
    {
        public BabySitter()
        {
            Requests = new HashSet<Request>();
            Reviews = new HashSet<Review>();
        }

        public int BabySitterId { get; set; }
        public int UserId { get; set; }
        public int RatingAverage { get; set; }
        public bool HasCar { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
