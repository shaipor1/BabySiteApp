using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class Area
    {
        public Area()
        {
            Cities = new List<City>();
        }

        public int AreaId { get; set; }
        public int AreaName { get; set; }

        public virtual List<City> Cities { get; set; }
    }
}
