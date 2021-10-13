using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class MassageType
    {
        public MassageType()
        {
            Massages = new HashSet<Massage>();
        }

        public int MassageTypeId { get; set; }
        public int UserTypeId { get; set; }
        public string MassageTypeName { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<Massage> Massages { get; set; }
    }
}
