using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Test
    {
        static int Sum(params int[] ints)
        {
            int sum = 0;
            for (int i = 0; i < ints.Length; i++)
                sum += ints[i];
            return sum;
        }

        static void Main(string[] args)
        {
            int total = Sum(1, 2, 3, 4);
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
