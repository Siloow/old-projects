using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    interface Vehicle
    {
        bool move();
        void loadFuel(Fuel type);
    }
    interface Fuel
    {
        bool checkFuel();
    }
    class Car: Vehicle
    {
        private int enoughFuel;

        public bool move()
        {
            if (enoughFuel > 0) {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void loadFuel(Fuel type)
        {

        }
    }
    class Gasoline: Fuel
    {

    }

}
