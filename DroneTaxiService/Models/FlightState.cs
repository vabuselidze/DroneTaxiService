using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Models
{
    class FlightState : IStateContext
    {

        public FlightAltitude FlightAltitude { get; set; }
        //Time unit is Minute
        public int TimeLeftTillLanding { get; set; }
        public int FlightDuration { get; set; }






        public List<string> GetStateInfo()
        {
            var resultList = new List<string>() { "On flight state" };

            if (TimeLeftTillLanding == FlightDuration)
            {
                resultList.Add($"Approved to take-off at {FlightAltitude.Name}");
            }


            resultList.Add($"Time left till landing -  { TimeLeftTillLanding} minute(s);");


            return resultList;
        }


        //Not required implementation
        public void MakeLogs()
        {
            throw new NotImplementedException();
        }
    }
}
