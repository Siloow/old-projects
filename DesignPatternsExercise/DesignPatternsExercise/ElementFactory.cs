using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    abstract class ElementFactory
    {
        public abstract Button Create(int id, Texture2D texture, Action action);
    }
    class ElementsFactory : ElementFactory
    {
        public override Button Create(int id, Texture2D texture, Action action)
        {
            if ((id == 1))
            {
                return new Button(Color.Red, new Point(200, 200), new Point(50, 50), texture, action);
            }
            if ((id == 2))
            {
                return new Label(Color.White, ;
            }
            throw new Exception("Wrong input");
        }

    }
}
