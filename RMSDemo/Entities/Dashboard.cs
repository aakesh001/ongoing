using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Dashboard
    {
        public int SrNo { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string IMEI { get; set; }
        public string Frquency { get; set; }
        public string DcBusVoltage { get; set; }
        public string Current { get; set; }
        public string InPower { get; set; }
        public string OutVolt { get; set; }
        public string LastUpdate { get; set; }
        public string FarmerName { get; set; }
        public string Pono { get; set; }
        public string District { get; set; }
        public string PhoneNumber { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public int MandalID { get; set; }
        public string MandalName { get; set; }
        public string TotalEnergy { get; set; }
        public string TotalWaterFLOw { get; set; }
    }
}