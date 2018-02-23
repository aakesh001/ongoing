using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Distributor
    {
        public int Id { get; set; }
        public string DistributorName { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string OrganizationName { get; set; }
        public string LogoPath { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DistributorID { get; set; }
    }
}