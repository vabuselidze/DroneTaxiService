using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Models
{
    class Params
    {
        public int DronesCount { get; set; } 
        public int FlightAltitudesCount { get; set; }

        // Speed unit -  miles/hour

        public double RiseUpSpeed { get; set; }
        public double DescendSpeed { get; set; }
        public double StraightLineFlySpeed { get; set; }

       
        //  Altitudes are initialized in Feets

        public int MinFlightAltitudeFeets { get; set; }
        public int MaxFlightAltitudeFeets { get; set; }

        public int MinDroneWaitTimeForPassenger { get; set; }
        public int MaxDroneWaitTimeForPassenger { get; set; }

        public double MinRequestedTravelDistance { get; set; }
        public double MaxRequestedTravelDistance { get; set; }

        public int MaxTickerCount { get; set; }

        //ticker interval in miliseconds
        public int Interval { get; set; }


    }
}
