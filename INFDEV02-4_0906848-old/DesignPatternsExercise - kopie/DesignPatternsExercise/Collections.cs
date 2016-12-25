using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    public class ElementsList<T> : IIterator<T>
    {
        private List<T> elemList;
        private int index = -1;
        public ElementsList(List<T> list)
        {
            this.elemList = list;
        }
        public IOption<T> GetNext()
        {
            index = (index + 1) % elemList.Count;
            return new Some<T>(elemList[index]);
        }
    }   
}
