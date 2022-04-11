using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Models
{
    class FlightAltitude
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Altitude { get; set; }
        public bool IsAvailable { get; set; }


    }
}
