using DroneTaxiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Services
{
   static  class ServiceEngine
    {

        static Params _params;

        static List<Drone> _allDrones;
        static List<FlightAltitude> _allFlightAltitudes;
        static List<FlightAltitude> _availableFlightAltitudes;

        public static void Initialize(Params initParams )
        {
            _params = initParams;
           
            //Drones

            _allDrones = new List<Drone>();
            for (int ind = 1; ind <= _params.DronesCount; ind++)
            {
                var newDrone = new Drone
                {
                    ID = ind,
                    Name = nameof(Drone) + "_" + ind
                };
                _allDrones.Add(newDrone);

                //State initialization
                SetGroundStateObject(newDrone, false);
            }

            //Flight Altitudes

            _allFlightAltitudes = new List<FlightAltitude>();
            _availableFlightAltitudes = new List<FlightAltitude>();

            for (int ind = 1; ind <= _params.FlightAltitudesCount; ind++)
            {
                var flightAltitude = new FlightAltitude
                {
                    ID = ind,
                    Name = nameof(FlightAltitude) + "_" + ind,
                    Altitude = Helper.GetRandomNumber(_params.MinFlightAltitudeFeets, _params.MaxFlightAltitudeFeets),
                    IsAvailable = true
                };

                _allFlightAltitudes.Add(flightAltitude);
                _availableFlightAltitudes.Add(flightAltitude);
            }



        }
        private static void SetNextState(Drone drone)
        {
            var droneState = drone.DroneState;

            //Drone current state - OnGround
            if (droneState is GroundState groundState)
            {
                groundState.IsLanded = groundState.IsLanded && (groundState.TimeLeftTillPassingerArrives == groundState.DroneWaitTimeForPassenger);
                groundState.TimeLeftTillPassingerArrives = Math.Max(0, groundState.TimeLeftTillPassingerArrives-1);
                if (groundState.RejectedToTakeOffCount > 0 || groundState.TimeLeftTillPassingerArrives == 0)
                {
                    //Not approved, because there is no available flight altitudes
                    if (!_availableFlightAltitudes.Any())
                    {
                        groundState.RejectedToTakeOffCount++;
                        return;
                    }

                    //Othervise approved  
                    SetFlightStateObbjectAndUpdateFlightAltitudes(drone);

                }
                return;
            }

            //Drone current state - OnFly
            if (droneState is FlightState flightState)
            {
                flightState.TimeLeftTillLanding--;
                if (flightState.TimeLeftTillLanding == 0)
                {
                    //Add flight altitude to available list
                    flightState.FlightAltitude.IsAvailable = true;
                    _availableFlightAltitudes.Add(flightState.FlightAltitude);

                    SetGroundStateObject(drone);

                }
                return;
            }


        }

        // It occurs when request is approved
        private static void SetFlightStateObbjectAndUpdateFlightAltitudes(Drone drone)
        {
            var availableFlightAltitudes = _availableFlightAltitudes.First();
            availableFlightAltitudes.IsAvailable = false;
            _availableFlightAltitudes.RemoveAt(0);
            int flightDurationInMinutes = (int)(Math.Ceiling(Helper.CalcMinutesForFeets(availableFlightAltitudes.Altitude, _params.RiseUpSpeed) +
                                 Helper.CalcMinutesForFeets(availableFlightAltitudes.Altitude, _params.DescendSpeed) +
                                 Helper.CalcMinutes(Helper.GetRandomNumber(_params.MinRequestedTravelDistance, _params.MaxRequestedTravelDistance), _params.StraightLineFlySpeed)));

            drone.DroneState = new FlightState()
            {
                FlightAltitude = availableFlightAltitudes,
                FlightDuration = flightDurationInMinutes,
                TimeLeftTillLanding = flightDurationInMinutes
            };
        }


        //  drone is landed or taking first time onGround
        private static void SetGroundStateObject(Drone drone, bool isLanded = true)
        {
            int waitTime = Helper.GetRandomInteger(_params.MinDroneWaitTimeForPassenger, _params.MaxDroneWaitTimeForPassenger);
            drone.DroneState = new GroundState
            {
                DroneWaitTimeForPassenger = waitTime,
                IsLanded = isLanded,
                RejectedToTakeOffCount = 0,
                TimeLeftTillPassingerArrives = waitTime
            };
        }

        public  static void Simulation()
        {
            // Set Next states for drones with Flight State to avoid conflict with available flight altitudes 
            var listFlightStateDrones = _allDrones.Where(dr => dr.DroneState is FlightState ).ToList();
 

            //Drones ordered by desc rejected count and wait time duration
            var listGraundStateDrones = _allDrones
                .Where(dr => dr.DroneState is GroundState
                )
                .OrderByDescending(gr => ((GroundState)(gr.DroneState)).RejectedToTakeOffCount)
                .ThenByDescending(gr => ((GroundState)(gr.DroneState)).DroneWaitTimeForPassenger)
                .ToList();

            listFlightStateDrones.ForEach(dr =>
            {
                SetNextState(dr);
            });

            //Shuffle available Flight Altitudes
            _availableFlightAltitudes = _availableFlightAltitudes.OrderBy(fl => Guid.NewGuid()).ToList();

            listGraundStateDrones.ForEach(dr =>
            {
                SetNextState(dr);
            });

        }

        // output
        public static void MakeOutput(int simIndex)
        {
            string strLine= new string('-', 100);
            Console.WriteLine(strLine);
            _allDrones.ForEach(dr =>
            {

                var _listStrings = dr.DroneState.GetStateInfo();
                _listStrings.ForEach(str =>
                {
                    Console.WriteLine($"{DateTime.Now} || SM {simIndex} || {dr.Name} || {str} ");
                });
                Console.WriteLine(strLine);

            });
            


        }



    }
}
