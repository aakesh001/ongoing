using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMSDemo.Models
{
    public class FarmerViewModel
    {
        public List<RegisteredDevice> Devices { get; set; }
        public List<State> States { get; set; }
        public List<Village> Villages { get; set; }
        public List<Mandal> Mandal { get; set; }
        public List<Company> Companies { get; set; }
    }
}