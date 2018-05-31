using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Farmer : RegisteredDevice
    {
        public int Id { get; set; }
        public string FarmerName { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Pono { get; set; }
        public string DistrictId { get; set; }
        public string PhoneNumber { get; set; }
        public int StateId { get; set; }
        public int VillageId { get; set; }
        public int MadnalId { get; set; }
        public string StateName { get; set; }
        public string VillageName { get; set; }
        public string MandalName { get; set; }
        public string Department { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public int DistId { get; set; }
        public string FarmerCompanyName { get; set; }
        public string DistributorFirstLastName { get; set; }
    }
}