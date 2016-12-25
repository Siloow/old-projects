using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    class Counter
    {
        public int count { get; set; }

        public Counter()
        {
            this.count = 0;
        }

        public void Reset()
        {
            count = 0;
        }

        public void Tick()
        {
            count += 1;
        }

        static public Counter operator+ (Counter b, Counter c)
        {
            Counter counter = new Counter();
            counter.count = b.count + c.count;
            return counter;
        }


    }
}