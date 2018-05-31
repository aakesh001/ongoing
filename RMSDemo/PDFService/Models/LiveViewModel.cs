using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFService.Models
{
    public class LiveViewModel
    {
        public List<RegisteredDevice> Devices { get; set; }
        public string Username { get; set; }
        public string Path { get; set; }
    }
}