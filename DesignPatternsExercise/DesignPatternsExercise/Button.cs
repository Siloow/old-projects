using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DesignPatternsExercise
{
    public class Button : IElement
    {
        public Button(Color color, Point location, Point size)
        {
            this.Color = color;
            this.Location = location;
            this.Size = size;
        }
        public Color Color { get; }

        public Point Location { get; }

        public Point Size { get; }

        public Rectangle DrawRectangle { get { return new Rectangle(Location, Size); } }

        public void Draw(float dt)
        {
            throw new NotImplementedException();
            // draw methode hier
        }

        public void Update(SpriteBatch spriteBatch)
        {
        }
    }
}
