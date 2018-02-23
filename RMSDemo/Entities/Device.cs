using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OutPutFrequency { get; set; }
        public string DCBusVoltage { get; set; }
        public string OutPutCurrent { get; set; }
        public string InputPower { get; set; }
        public string OutPutVoltage { get; set; }
        public string OutPutFreqFormat { get; set; }
        public string DcBusVoltageFormat { get; set; }
        public string OutPutCurrentFormat { get; set; }
        public string InputPowerFormat { get; set; }
        public string OutPutVolatageFormat { get; set; }
        public string BaudRate { get; set; }
        public string Parity { get; set; }
        public string StopBit { get; set; }
    }
}