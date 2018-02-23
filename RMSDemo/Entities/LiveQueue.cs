using System;

namespace Entities
{
    public class LiveQueue
    {
        public string imei { get; set; }
        public int device_id { get; set; }
        public decimal frequency { get; set; }
        public decimal dc_bus_voltage { get; set; }
        public decimal current { get; set; }
        public decimal in_power { get; set; }
        public decimal output_voltage { get; set; }
        public DateTime last_update { get; set; }
        public string lattitude { get; set; }
        public string longitude { get; set; }
    }
}