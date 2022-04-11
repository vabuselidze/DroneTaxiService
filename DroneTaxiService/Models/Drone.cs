using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Models
{
    class Drone
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public IStateContext DroneState { get; set; }
    }
}
