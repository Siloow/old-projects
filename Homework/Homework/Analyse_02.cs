using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class calculateDoekoes
    {
        int Doekoes;
        int maxPerDag = 2000;
        int minRood = -500;
        int saldo = 1500;
        bool genoegGeldAutomaat = true;

        public calculateDoekoes(int doekoes)
        {
            this.Doekoes = doekoes;
        }

        public void calculateResult()
        {         
            if (genoegGeldAutomaat)
            {
                if (Doekoes > saldo)
                {
                    Console.WriteLine("Niet genoeg saldo");
                }
                else
                {
                    if ((saldo - Doekoes) > minRood)
                    {
                        Console.WriteLine("U krijgt {0} euro", Doekoes);
                    }
                    else if (!(Doekoes < maxPerDag)){
                        Console.WriteLine("Max bedrag overschreden");
                    }
                    else
                    {
                        Console.WriteLine("Niet genoeg saldo");
                    }
                }
            }
            else
            {
                Console.WriteLine("Niet genoeg geld beschikbaar");
            }             
        }
    }
}
