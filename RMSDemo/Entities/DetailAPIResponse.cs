using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DetailAPIResponse
    {
        public string imei { get; set; }
        public Int32 device_id { get; set; }
        public Int32 output_frequency { get; set; }
        public Int32 dc_bus_voltage { get; set; }
        public Int32 output_current { get; set; }
        public Int32 input_power { get; set; }
        public Int32 output_voltage { get; set; }
        public Int32 output_freq_multi { get; set; }
        public Int32 dc_bus_voltage_multi { get; set; }
        public Int32 output_current_multi { get; set; }
        public Int32 input_power_multi { get; set; }
        public Int32 output_voltage_multi { get; set; }
        public Int32 interval { get; set; }
        public Int32 baud { get; set; }
        public Int32 parity { get; set; }
        public Int32 stopbit { get; set; }
    }
}