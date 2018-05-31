using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RegisteredDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public string IMEI { get; set; }
        public string RegisteredDeviceId { get; set; }
        public bool Enabled { get; set; }
        public string MotorEfficiency { get; set; }
        public string Head { get; set; }
        public string MotorPower { get; set; }
        public bool MotorControl { get; set; }
        public string BenificiaryName { get; set; }
        public string WorkOrderNo { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string VFD { get; set; }
        public string Address { get; set; }
        public string Interval { get; set; }
        public string DistributorName { get; set; }
        public string CompanyName { get; set; }
        public string Lan { get; set; }
        public string Lon { get; set; }
        public string Ccid { get; set; }
        public string OpName { get; set; }
        public string Signal { get; set; }
        public string Serial { get; set; }
        public string STime { get; set; }
        public string Btime { get; set; }
    }

    public class RebootReset
    {
        public string Reboot { get; set; }
        public string Reset { get; set; }
    }
}