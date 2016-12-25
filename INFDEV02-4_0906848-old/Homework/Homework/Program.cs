using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kaas;
using System.Threading;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Test exercise 1
            //INumberVisitor nv = new NumberVisitor();

            //INumber i = new MyInt();

            //i.Visit(nv);
            //Console.ReadLine();

            //// Test exercise 2
            //MusicLibraryVisitor mlv = new MusicLibraryVisitor();
            //List<ISong> songs = new List<ISong>();
            //songs.Add(new Jazz("Nu Nu"));
            //foreach(var song in songs)
            //{
            //    song.Visit(mlv);
            //}
            //Console.WriteLine(mlv.jazz.ToString());


            //// Test exercise 3
            //IOption<int> i = new Some<int>(5);

            //int incr = i.Visit(() => { throw new Exception("kaas?"); }, a => a + 1);
            //Console.WriteLine(incr);
            //Console.ReadLine();


            //// Test adapter
            //kaas.TraditionalIterator<int> elems = new kaas.UnsafeCollections.NaturalList();
            //elems.MoveNext();
            //while (true)
            //{
            //    Console.WriteLine(elems.Current);
            //    elems.MoveNext();
            //    Thread.Sleep(100);
            //}

            //while (true)
            //{
            //    Console.WriteLine("Met hoeveel personen bent u?");
            //    int result = Int32.Parse(Console.ReadLine());

            //    if (!(result > 0 && result <= 250))
            //    {
            //        Console.WriteLine("The minimum amount of people is 0 and the maximum is 250");
            //    }
            //    else
            //    {
            //        calculateCosts a = new calculateCosts(result);

            //        if (result == 1)
            //        {
            //            Console.WriteLine("\nIn totaal bent u {0} euro kwijt voor {1} persoon", a.generateResult(), result);
            //        }
            //        else
            //        {
            //            Console.WriteLine("\nIn totaal bent u {0} euro kwijt voor {1} personen", a.generateResult(), result);
            //        }
            //        Console.ReadLine();
            //        break;
            //    }
            //}
            //while (true)
            //{
            //    Console.WriteLine("Hoeveel geld wilt u opnemen?");
            //    string answer = Console.ReadLine();
            //    int number = 0;
            //    bool isNum = int.TryParse(answer, out number);

            //    if (!(isNum)) // Check de user input
            //    {
            //        Console.WriteLine("Please enter a correct input");
            //    }
            //    else
            //    {
            //        calculateDoekoes a = new calculateDoekoes(number);
            //        a.calculateResult();
            //        Console.ReadLine();
            //        break;
            //    }
            //}
        }
    }
}
