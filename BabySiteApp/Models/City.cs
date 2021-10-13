using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public int CityId { get; set; }
        public int CityName { get; set; }
        public int AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
