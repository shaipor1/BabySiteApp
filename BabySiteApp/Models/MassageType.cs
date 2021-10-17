using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class MassageType
    {
        public MassageType()
        {
            Massages = new List<Massage>();
        }

        public int MassageTypeId { get; set; }
        public int UserTypeId { get; set; }
        public string MassageTypeName { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual List<Massage> Massages { get; set; }
    }
}
