using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class User
    {
        public User()
        {
            BabySitters = new List<BabySitter>();
            Massages = new List<Massage>();
            Parents = new List<Parent>();
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
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }


        public virtual Location Location { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual List<BabySitter> BabySitters { get; set; }
        public virtual List<Massage> Massages { get; set; }
        public virtual List<Parent> Parents { get; set; }
    }
}
