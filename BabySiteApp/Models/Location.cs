using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class Location
    {
        public Location()
        {
            Users = new List<User>();
        }

        public int LocationId { get; set; }
        public int CityId { get; set; }
        public int HouseId { get; set; }
        public string Street { get; set; }

        public virtual City City { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
