using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public DateTime? CapturedDateTime { get; set; }
        public string Token { get; set; }
    }
}
