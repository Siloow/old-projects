using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    interface IIterator<T>
    {
        IOption<T> GetNext();
        
    }
    public interface TraditionalIterator<T>
    {
        bool MoveNext();
        T Current { get; }
    }
}
