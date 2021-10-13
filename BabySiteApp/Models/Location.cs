using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class Location
    {
        public Location()
        {
            Users = new HashSet<User>();
        }

        public int LocationId { get; set; }
        public int CityId { get; set; }
        public int HouseId { get; set; }
        public string Street { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
