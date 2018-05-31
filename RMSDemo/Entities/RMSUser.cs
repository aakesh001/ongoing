using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RMSUser
    {
        public RMSUser()
        { }

        public RMSUser(string firstName, string lastName, string userName, string passWord, string email, string phoneNumber, string roleID)
        {
            this.FirstName = firstName; this.LastName = lastName;
            this.UserName = userName; this.Password = passWord;
            this.Email = email; this.PhoneNumber = phoneNumber; this.RoleID = roleID;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string LogoPath { get; set; }
    }
}