using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    public interface IOption<T>
    {
        U Visit<U>(Func<U> onNone, Func<T, U> onSome);
    }
}
