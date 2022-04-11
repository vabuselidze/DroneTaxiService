using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Models
{
    class GroundState : IStateContext
    {

        //Time unit is Minute
        public int TimeLeftTillPassingerArrives {get; set;}

        //Random time between 1 and 15  (in minutes)
        public int DroneWaitTimeForPassenger { get; set; }
        public bool IsLanded { get; set; }
        public int RejectedToTakeOffCount { get; set; }



        public List<string> GetStateInfo()
        {
            var resultList = new List<string>();
            if (IsLanded) 
                resultList.Add("Landed;");

            resultList.Add("On ground state;");

            if (TimeLeftTillPassingerArrives > 0)
            {
                resultList.Add($"Time left till a passinger arrives - {TimeLeftTillPassingerArrives} minute(s);");
            }
            else
            {
                resultList.Add($"Passenger arrived to drone but request was declined  {RejectedToTakeOffCount} time(s);");
            }

            return resultList;

        }


        //Not required implementation
        public void MakeLogs()
        {
            throw new NotImplementedException();
        }
    }
}
