using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DesignPatternsExercise
{
    public class Button : IElement
    {
        public Button(Color color, Point location, Point size, Texture2D texture, Action action)
        {
            this.Color = color;
            this.Location = location;
            this.Size = size;
            this.Texture = texture;
            this.Action = action;
        }
        public Color Color { get; }
        public Point Location { get; }
        public Point Size { get; }
        public Texture2D Texture { get; }
        public Rectangle DrawRectangle { get { return new Rectangle(Location, Size); } }
        public Action Action { get; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawRectangle, Color.White);

        }
        public void Visit(IElementVisitor visitor)
        {
            visitor.onButton(this);
        }

        public void Update(float dt)
        {
            MouseState input = Mouse.GetState();
            Point mousePosition = new Point(input.X, input.Y);
            if (DrawRectangle.Contains(mousePosition) && input.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("clicked");
                // change color of button or text?
            }
        }
    }
    public class Label : IElement
    {
        public Label(Color color, Point location, Point size, Action action, string text, SpriteFont font)
        {
            this.Color = color;
            this.Location = location;
            this.Size = size;
            this.Action = action;
            this.Text = text;
            this.Font = font;
        }
        public Color Color { get; }
        public Point Location { get; }
        public Point Size { get; }
        public string Text { get; }
        public Rectangle DrawRectangle { get { return new Rectangle(Location, Size); } }
        public Action Action { get; }
        public SpriteFont Font { get; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString()
        }
        public void Visit(IElementVisitor visitor)
        {
            visitor.onLabel(this);
        }

        public void Update(float dt)
        {
            // do something
        }
    }
}
