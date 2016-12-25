using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowYourMove
{
    // This class calculats the distance between two longitudes, latitudes
    public class CalcDistance
    {
        const double PI = 3.141592653589793; // Short Pi
        const double rad = 6378.16; // Earth's radius

        public CalcDistance()
        {

        }
        public static double Radians(double x)
        {
            return x * PI / 180;
        }
        public static double DistanceBetweenPlaces(
                double lon1,
                double lat1,
                double lon2,
                double lat2)
        {
            double dlon = Radians(lon2 - lon1);
            double dlat = Radians(lat2 - lat1);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * rad;
        }
    }
}
