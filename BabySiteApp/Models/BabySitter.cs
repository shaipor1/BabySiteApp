using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class BabySitter
    {
        public BabySitter()
        {
            Requests = new List<Request>();
            Reviews = new List<Review>();
        }

        public int BabySitterId { get; set; }
        public int UserId { get; set; }
        public int RatingAverage { get; set; }
        public bool HasCar { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public virtual User User { get; set; }
        public virtual List<Request> Requests { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
