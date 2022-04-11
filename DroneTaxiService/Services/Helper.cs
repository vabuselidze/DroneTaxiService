using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTaxiService.Services
{
    static class Helper
    {
        const double MILES_TO_FEET_RATIO = 5280;

        static  Random rnd = new ();

        public static int GetRandomInteger(int  min , int max)
        {
            return rnd.Next(min, max + 1);
        }

        public static double GetRandomNumber(double min, double max)
        {
            return rnd.NextDouble() * ( max- min) + min;         
        }

        public static double ConvertFeetsToMiles(double feet)
        {
            return (feet / MILES_TO_FEET_RATIO);
        }

        public static double CalcMinutes(double miles , double speedMilesPerHour)
        {
            return 60 * miles / speedMilesPerHour;
        }

        public  static double CalcMinutesForFeets(double feets, double speedMilesPerHour)
        {
            return CalcMinutes( ConvertFeetsToMiles(feets) , speedMilesPerHour );
        }


    }
}
