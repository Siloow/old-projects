using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    abstract class ElementFactory
    {
        public abstract Button Create(int id);
    }
    class ElementsFactory : ElementFactory
    {
        public override Button Create(int id)
        {
            if ((id == 1))
            {
                return new Button(Color.Red, new Point(200, 200), new Point(50, 50));
            }
            if ((id == 2))
            {
                return new Button(Color.Yellow, new Point(250, 250), new Point(150, 40));
            }
            throw new Exception("Wrong input");
        }

    }
}
