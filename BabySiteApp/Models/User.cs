using System;
using System.Collections.Generic;
using BabySiteApp.Services;



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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPswd { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual List<BabySitter> BabySitters { get; set; }
        public virtual List<Massage> Massages { get; set; }
        public virtual List<Parent> Parents { get; set; }
        //Added only to client side
        public string PhotoURL
        {
            get
            {
                BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
                Random r = new Random();
                return $"{proxy.GetBasePhotoUri()}{this.UserId}.jpg?" + r.Next();
            }
        }
    }
}
