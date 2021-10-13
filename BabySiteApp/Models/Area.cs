using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class Area
    {
        public Area()
        {
            Cities = new HashSet<City>();
        }

        public int AreaId { get; set; }
        public int AreaName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
