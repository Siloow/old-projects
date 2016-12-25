using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    interface ICustomer 
    {
        int Id { get; }
        Gender gender { get; }
    }
}
