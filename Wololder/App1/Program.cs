using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Box
    {
        private double length;
        private double breadth;
        private double height;

        public Box()
        {
            Console.WriteLine("Object box is being created");
        }
        ~Box()
        {
            Console.WriteLine("Object box is being deleted");
        }

        public void setLength(double len)
        {
            length = len;
        }

        public void setBreadth(double bre)
        {
            breadth = bre;
        }
        
        public void setHeight(double hei)
        {
            height = hei;
        }

        public double getVolume()
        {
            return length * breadth * height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Box box1 = new Box();
            Box box2 = new Box();
            double volume;

            box1.setBreadth(6.0);
            box1.setHeight(6.0);
            box1.setLength(6.0);

            volume = box1.getVolume();

            Console.WriteLine(volume);
            Console.ReadKey();
        }
    }
}
