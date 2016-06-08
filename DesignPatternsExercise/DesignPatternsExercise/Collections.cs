using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    public class ElementsList<T> : TraditionalIterator<T>
    {
        private List<T> elemList;
        private int index = -1;
        public ElementsList(List<T> list)
        {
            this.elemList = list;
        }

        public T Current
        {
            get
            {
                if (index < 0)
                    throw new Exception("Do move next first");
                return elemList[index];
            }
        }

        public bool MoveNext()
        {
            if (index + 1 < elemList.Count)
                index = 0;
            else
                index++;
            return true;
        }
    }   
}
