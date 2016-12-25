using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    interface IRepository
    {
        void Add(ICustomer c);
        void Remove(ICustomer c);
        void Update(ICustomer c, ICustomer newC);
        IOption<ICustomer> GetCustomer(int id);
    }
}
