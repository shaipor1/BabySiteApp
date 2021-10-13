using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class User
    {
        public User()
        {
            BabySitters = new HashSet<BabySitter>();
            Massages = new HashSet<Massage>();
            Parents = new HashSet<Parent>();
        }

        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int LocationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPswd { get; set; }

        public virtual Location Location { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<BabySitter> BabySitters { get; set; }
        public virtual ICollection<Massage> Massages { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
    }
}
