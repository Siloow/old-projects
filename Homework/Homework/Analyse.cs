using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class calculateCosts
    {
        int amountOfPeople = 0;
        bool isAbbo = false; // Is de gebruiker een abbonnementhouder?
        bool isBeg = false; // Is de gebruiker een begeleider?

        public calculateCosts(int amount)
        {
            this.amountOfPeople = amount;
        }

        public float generateResult()
        {
            Dictionary<int, float> totalCost = new Dictionary<int,float>(); // Een dictionary met incrementing keys en bijbehorende prijs per persoon
            for (int i = 1; i <= amountOfPeople; i++)
            {
                while (true)
                {
                    Console.WriteLine("Is persoon {0} abbonnementhouder? [Y/N]", i);
                    string abboAnswer = Console.ReadLine().ToUpper();
                    if (abboAnswer != "Y" && abboAnswer != "N") // Check de user input
                    {
                        Console.WriteLine("Please enter a correct input [Y/N]");
                    }
                    else
                    {
                        if (abboAnswer == "Y")
                        {
                            isAbbo = true;
                            break; // Break out the while
                        }
                        else if (abboAnswer == "N")
                        {
                            isAbbo = false;
                            break; // Break out the while
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                while (true)
                {
                    Console.WriteLine("Is persoon {0} een begeleider voor een gehandicapte?[Y/N]", i);
                    string begAnswer = Console.ReadLine().ToUpper(); ;
                    if (begAnswer != "Y" && begAnswer != "N") // Check de user input
                    {
                        Console.WriteLine("Please enter a correct input [Y/N]");
                    }
                    else
                    {
                        if (begAnswer == "Y")
                        {
                            isBeg = true;
                            break; // Break out the while
                        }
                        else if (begAnswer == "N")
                        {
                            isBeg = false;
                            break; // Break out the while
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                while (true)
                {
                    Console.WriteLine("Hoe oud is persoon {0}", i);
                    int result = Int32.Parse(Console.ReadLine());

                    if (!(result >= 0 && result <= 150))
                    {
                        Console.WriteLine("Please enter a correct age");
                    }
                    else
                    {
                        // Een stel if's die de kosten bepalen en toevoegen aan de lijst
                        if (isBeg && result > 13)
                        {
                            float cost = 12.00f;
                            totalCost.Add(i, cost);
                        }
                        else if (result >= 0 && result <= 2)
                        {
                            float cost = 0.0f;
                            totalCost.Add(i, cost);
                        }
                        else if (result >= 3 && result <= 12)
                        {
                            float cost = 17.50f;
                            totalCost.Add(i, cost);
                        }
                        else if (result > 12)
                        {
                            float cost = 22.00f;
                            totalCost.Add(i, cost);
                        }

                        if (isAbbo) // Bereken korting over de kosten als het een abbonementhouder is
                        {
                            Console.WriteLine("Een korting van 25% is berekend over deze persoon omdat diegene een abbonnementhouder is");
                            totalCost[i] = totalCost[i] * 0.75f;
                        }

                        if (i != amountOfPeople) // Alleen naar volgend persoon vragen als het niet de laatste persoon is
                        {
                            Console.WriteLine("\n ~~~~~Volgende persoon~~~~~");
                        }
                        break;
                    }
                }
            }

            float sum = 0; 
            for (int i = 1; i <= totalCost.Count; i++) // Tel alle kosten bij elkaar op voor de totaal kosten
            {
                sum += totalCost[i];
            }

            if (amountOfPeople >= 20) // Bereken een korting van 2 euro p.p als er meer dan 20 personen zijn
            {
                Console.WriteLine("Een korting van 2 euro per persoon is berekend");
                sum -= amountOfPeople * 2.00f;
            }
            return sum;
        }
    }
}
