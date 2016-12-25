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
            Animal myAnimal = new Cat();
            myAnimal.makeSound();
            Console.ReadLine();
        }
    }

}
