using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class UserType
    {
        public UserType()
        {
            MassageTypes = new HashSet<MassageType>();
            Users = new HashSet<User>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public virtual ICollection<MassageType> MassageTypes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
