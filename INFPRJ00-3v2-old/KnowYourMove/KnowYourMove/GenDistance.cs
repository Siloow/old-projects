using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowYourMove
{
    public class GenDistance
    {
        public GenDistance()
        {
        }

        public static double getDistance(int userinput) // This extends CalcDistance.cs and is only used to query against the database and return the distance between two given points
        {
            int userInput = userinput;
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                // Calculate distance to central
                var speedPostalLat = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.centralelat).SingleOrDefault();
                var speedPostalLong = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.centralelong).SingleOrDefault();

                var locationPostalLat = context.SpeedLocations.Where(f => f.postcode == userInput).Select(c => c.cnlat).SingleOrDefault();
                var locationPostalLong = context.SpeedLocations.Where(f => f.postcode == userInput).Select(c => c.cnlng).SingleOrDefault();

                double afstand = CalcDistance.DistanceBetweenPlaces((double)speedPostalLong, (double)speedPostalLat, (double)locationPostalLong, (double)locationPostalLat);

                return afstand;
            }
        }
    }
}
