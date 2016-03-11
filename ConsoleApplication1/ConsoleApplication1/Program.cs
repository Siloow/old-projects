using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal cat = new Cat();
            Animal dog = new Dog();
            Animal cow = new Cow();

            Animal[] myList = { cat, dog, cow };

            loopThroughAnimals(myList);
            Console.ReadLine();
                     
        }
        static void loopThroughAnimals(Animal[] aList)
        {
            foreach (Animal i in aList)
            {
                i.saySomething();
            }
        }
    }
}
