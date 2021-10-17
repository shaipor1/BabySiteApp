using System;
using System.Collections.Generic;



namespace BabySiteApp.Models
{
    public partial class UserType
    {
        public UserType()
        {
            MassageTypes = new List<MassageType>();
            Users = new List<User>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public virtual List<MassageType> MassageTypes { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
