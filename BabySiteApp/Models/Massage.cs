using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class Massage
    {
        public Massage()
        {
            Requests = new List<Request>();
        }

        public int MassageId { get; set; }
        public int MassageTypeId { get; set; }
        public int UserId { get; set; }
        public string HeadLine { get; set; }
        public string Body { get; set; }

        public virtual MassageType MassageType { get; set; }
        public virtual User User { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}
