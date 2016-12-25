using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekkeroefenen
{
    class UserStory
    {
        private int Hours;
        private String Description;

        public int getHours()
        {
            return Hours;
        }

        public void setHours(int Hours)
        {
            this.Hours = Hours;
        }

        public String getDescription()
        {
            return Description;
        }

        public void setDescription(String Description)
        {
            this.Description = Description;
        }


        public void toString()
        {

        }



    }
}
