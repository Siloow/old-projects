using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] Numbers = { 1, 2, 3, 4 };
            IntArrayOpperations a = new IntArrayOpperations(Numbers);
            Console.WriteLine(a.ProductArray());
            Console.ReadLine();
        }
    }

}
