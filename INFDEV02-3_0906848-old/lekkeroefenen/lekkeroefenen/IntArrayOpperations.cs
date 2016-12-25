using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    class IntArrayOpperations
    {
        private int[] Numbers;

        public IntArrayOpperations(int[] Numbers)
        {
            this.Numbers = Numbers;
        }

        public int SumArray()
        {
            int Total = 0;
            foreach (int Item in Numbers)
            {
                Total += Item;
            }
            return Total;
        }

        public int ProductArray()
        {
            int Total = 1;
            foreach(int Item in Numbers)
            {
                Total = Total * Item;
            }
            return Total;
        }
    }
}
