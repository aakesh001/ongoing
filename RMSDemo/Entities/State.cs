using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
    }

    public class Village
    {
        public int Id { get; set; }
        public string VillageName { get; set; }
        public int StateId { get; set; }
    }

    public class Mandal
    {
        public int Id { get; set; }
        public string MandalName { get; set; }
        public int VillageId { get; set; }
        public int StateId { get; set; }
    }
}