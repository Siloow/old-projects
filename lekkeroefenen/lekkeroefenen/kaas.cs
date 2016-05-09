using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    interface Animal
    {
        void makeSound();
    }

    class Dog : Animal
    {
        public Dog()
        {
        }
        public void makeSound()
        {
            Console.WriteLine("Woof!");
        }
    }

    class Cat : Animal
    {
        public Cat()
        {
        }

        public void makeSound()
        {
            Console.WriteLine("Miao!");
        }
    }
}
