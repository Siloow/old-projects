using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    class Customer : ICustomer
    {
        public int Id { get; private set; }
        public Gender gender { get; private set; }
        
        public Customer(int id, Gender gender)
        {
            this.Id = id;
            this.gender = gender;
        }
    }

}
