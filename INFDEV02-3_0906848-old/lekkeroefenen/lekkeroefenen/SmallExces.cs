using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    class SmallExces
    {
        // Exercise 1.1.1
        public static void driehoekje()
        {
            string result = "";

            for (int a = 0; a < 10; a = a + 1)
            {
                for (int b = 0; b < a; b = b + 1)
                {
                    result += "*";
                }
                result += "\n";
            }
            Console.Write(result);

        }
        // Exercise 1.1.2
        public static void smiley()
        {
            string result = "";
            for (int a = 0; a < 10; a = a + 1)
            {
                for (int b = 0; b < 10; b = b + 1)
                {
                    if ((a == 2 && b == 4) || (a == 2 && b == 6))
                    {
                        result += "*";
                    }
                    if ((a == 4 && b == 2) || (a == 4 && b == 8))
                    {
                        result += "*";
                    }
                    if ((a == 5 && (b >= 3 && b <= 8)))
                    {
                        result += "*";
                    }
                    else
                    {
                        result += " ";
                    }

                }
                result += "\n";
            }
            Console.Write(result);
        }
        // Exercise 1.1.3
        public static void errorlol()
        {
            int halloIkBenEenVar = 0;

            string bier = halloIkBenEenVar + "kaas";

            Console.Write(bier);

        }
        // Exercise 1.1.4
        public static int sum(int start, int end)
        {
            if (start < end)
            {
                return start + sum(start + 1, end);
            }
            else
            {
                return 0;
            }
        }
    }
}
