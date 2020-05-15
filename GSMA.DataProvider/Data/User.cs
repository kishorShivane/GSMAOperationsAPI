using System;
using System.Collections.Generic;

namespace GSMA.DataProvider.Data
{
    public partial class User
    {
        public User()
        {
            EgmsealsAssaignedUser = new HashSet<Egmseals>();
            EgmsealsCapturedUser = new HashSet<Egmseals>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public DateTime CapturedDateTime { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<Egmseals> EgmsealsAssaignedUser { get; set; }
        public virtual ICollection<Egmseals> EgmsealsCapturedUser { get; set; }
    }
}
