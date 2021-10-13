using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class Massage
    {
        public Massage()
        {
            Requests = new HashSet<Request>();
        }

        public int MassageId { get; set; }
        public int MassageTypeId { get; set; }
        public int UserId { get; set; }
        public string HeadLine { get; set; }
        public string Body { get; set; }

        public virtual MassageType MassageType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
