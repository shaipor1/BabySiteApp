using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            Requests = new List<Request>();
        }

        public int RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }

        public virtual List<Request> Requests { get; set; }
    }
}
