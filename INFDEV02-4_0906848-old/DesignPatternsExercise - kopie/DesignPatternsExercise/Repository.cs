using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    class Repository : IIterator<ICustomer>, IRepository
    {
        List<ICustomer> customerList;
        int default_index { get; }
        int index;

        public Repository()
        {
            this.customerList = new List<ICustomer>();
        }
        void Reset()
        {
            this.index = this.default_index;
        }

        public IOption<ICustomer> GetNext()
        {
            index = (index + 1) % customerList.Count;
            return new Some<ICustomer>(customerList[index]);
        }

        public void Remove(ICustomer c)
        {
            customerList.Remove(c);
        }
        public void Update(ICustomer c, ICustomer newC)
        {
            if (!(c.gender == newC.gender && c.Id == newC.Id))
            {
                c = newC;
            }
        }
        public IOption<ICustomer>GetCustomer(int id)
        {
            return new Some<ICustomer>(customerList[id]);
        }

        public void Add(ICustomer c)
        {
            customerList.Add(c);
        }
    }
}
