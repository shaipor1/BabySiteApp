using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new List<Location>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual List<Location> Locations { get; set; }
    }
}
