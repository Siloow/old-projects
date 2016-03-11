using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    interface Animal
    {
        void saySomething();
    }

    class Cat: Animal
    {
        public void saySomething()
        {
            Console.WriteLine("Miao..");
        }
    }
    class Dog: Animal
    {
        public void saySomething()
        {
            Console.WriteLine("Boa..");
        }
    }
    class Cow: Animal
    {
        public void saySomething()
        {
            Console.WriteLine("Moo..");
        }
    }
}
