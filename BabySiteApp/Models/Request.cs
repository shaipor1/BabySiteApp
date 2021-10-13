using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public int ParentId { get; set; }
        public int BabySitterId { get; set; }
        public int MassageId { get; set; }
        public int RequestStatusId { get; set; }

        public virtual BabySitter BabySitter { get; set; }
        public virtual Massage Massage { get; set; }
        public virtual Parent Parent { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
    }
}
