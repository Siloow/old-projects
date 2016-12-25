using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    class Interval
    {
        int Start;
        int End;

        public Interval(int Start, int End)
        {
            this.Start = Start;
            this.End = End;
        }

        public int Sum()
        {
            int Total = 0;
            for (int a = this.Start; a < this.End; a++)
            {
                Total = Total + a;
            }
            return Total;

        }
        public int Product()
        {
            int Total = 1;
            for (int a = this.Start; a < this.End; a++)
            {
                Total = Total * a;
            }
            return Total;

        }
    }
}
