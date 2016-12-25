using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{

    // Exercise 1
    interface INumber
    {
        void Visit(INumberVisitor visitor);
    }

    interface INumberVisitor
    {
        void onMyInt(MyInt value);
        void onMyFloat(MyFloat value);
    }

    class MyFloat : INumber
    {
        public void Visit(INumberVisitor visitor)
        {
            visitor.onMyFloat(this);
        }
    }
    class MyInt : INumber
    {
        public void Visit(INumberVisitor visitor)
        {
            visitor.onMyInt(this);
        }
    }

    class NumberVisitor : INumberVisitor
    {
        public void onMyFloat(MyFloat value)
        {
            Console.WriteLine("Found float");
        }

        public void onMyInt(MyInt value)
        {
            Console.WriteLine("Found int");
        }
    }

    // Exercise 2

    interface ISong
    {
        void Visit(IMusicLibraryVisitor visitor);
    }
    interface IMusicLibraryVisitor
    {
        void onHeayMetal(HeavyMetal song);
        void onJazz(Jazz song);
    }

    class Jazz : ISong
    {
        string title;
        public Jazz(string title)
        {
            this.title = title;
        }
        public void Visit(IMusicLibraryVisitor visitor)
        {
            visitor.onJazz(this);
        }
    }
    class HeavyMetal : ISong
    {
        string title;
        public HeavyMetal(string title)
        {
            this.title = title;
        }
        public void Visit(IMusicLibraryVisitor visitor)
        {
            visitor.onHeayMetal(this);
        }
    }

    class MusicLibraryVisitor : IMusicLibraryVisitor
    {
        public List<HeavyMetal> heavyMetal = new List<HeavyMetal>();
        public List<Jazz> jazz = new List<Jazz>();

        public void onHeayMetal(HeavyMetal song)
        {
            heavyMetal.Add(song);
        }

        public void onJazz(Jazz song)
        {
            jazz.Add(song);
        }
    }

    //// Exercise 3

    //interface IOptionVisitor<T, U>
    //{
    //    U onSome(T value);
    //    U onNone();
    //}
    //interface IOption<T> { U Visit<U>(IOptionVisitor<T, U> visitor); }
    //class Some<T> : IOption<T>
    //{
    //    public T value;
    //    public Some(T value) { this.value = value; }
    //    public U Visit<U>(IOptionVisitor<T, U> visitor)
    //    {
    //        return visitor.onSome(this.value);
    //    }
    //}
    //class None<T> : IOption<T>
    //{
    //    public U Visit<U>(IOptionVisitor<T, U> visitor)
    //    {
    //        return visitor.onNone();
    //    }
    //}
    //class IntPrettyPrinterIOptionVisitor : IOptionVisitor<int, string>
    //{
    //    public string onNone()
    //    {
    //        return "There is nothing here";
    //    }
    //    public string onSome(int value)
    //    {
    //        return value.ToString();
    //    }
    //}
}

// Exercise 4
namespace kaas
{
    public interface IOption<T>
    {
        U Visit<U>(Func<U> onNone, Func<T, U> onSome);
    }
    public class Some<T> : IOption<T>
    {
        T value;
        public Some(T value) { this.value = value; }
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onSome(value);
        }
    }
    public class None<T> : IOption<T>
    {
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onNone();
        }
    }

    interface IIterator<T>
    {
        IOption<T> GetNext();
    }
    public interface TraditionalIterator<T>
    {
        bool MoveNext();
        T Current { get; }
    }

    // Safe collections
    namespace SafeCollections
    {
        public class NaturalList : IIterator<int>
        {
            private int current = -1;

            public IOption<int> GetNext()
            {
                current++;
                return new Some<int>(current);
            }
        }
        public class CircularList<T> : IIterator<T>
        {
            private List<T> list;
            private int index = -1;
            public CircularList(List<T> list)
            {
                this.list = list;
            }
            public IOption<T> GetNext()
            {
                index = (index + 1) % list.Count;
                return new Some<T>(list[index]);
            }
        }
        public class Array<T> : IIterator<T>
        {
            private T[] array;
            private int index = -1;
            public Array(T[] array)
            {
                this.array = array;
            }
            public IOption<T> GetNext()
            {
                if (index + 1 < array.Length)
                    return new None<T>();
                index++;
                return new Some<T>(array[index]);
            }
        }
    }

    namespace UnsafeCollections
    {
        public class NaturalList : TraditionalIterator<int>
        {
            private int current = -1;
            public int Current
            {
                get
                {
                    if (current < 0)
                        throw new Exception("MoveNext first.");
                    return current;
                }
            }
            public bool MoveNext()
            {
                current++;
                return true;
            }

            public void Reset() { current = -1; }
        }

        public class CircularList<T> : TraditionalIterator<T>
        {
            private List<T> list;
            private int index = -1;
            public CircularList(List<T> list)
            {
                this.list = list;
            }
            private CircularList() { }
            public T Current
            {
                get
                {
                    if (index < 0)
                        throw new Exception("MoveNext first.");
                    return list[index];
                }
            }
            public bool MoveNext()
            {
                if (index + 1 < list.Count)
                    index = 0;
                else
                    index++;
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }
        public class Array<T> : TraditionalIterator<T>
        {
            private T[] array;
            private int index = -1;
            public Array(T[] array)
            {
                this.array = array;
            }
            private Array()
            {
                
            }
            public T Current
            {
                get
                {
                    if (index < 0)
                        throw new Exception("Movenext first");
                    return array[index];
                }
            }
            public bool MoveNext()
            {
                if (index + 1 < array.Length)
                    return false;
                index++;
                return true;                                   
            }
            public void Reset()
            {
                index = -1;
            }
        }
        public class Map<T, U> : TraditionalIterator<U>
        {
            private TraditionalIterator<T> decoratedCollection;
            Func<T, U> f;
            public Map(TraditionalIterator<T> collection, Func<T, U> f)
            {
                this.decoratedCollection = collection;
                this.f = f;
            }

            public U Current
            {
                get
                {
                    return f(decoratedCollection.Current);
                }
            }
            public bool MoveNext()
            {
                return decoratedCollection.MoveNext();
            }
        }
    }
}

