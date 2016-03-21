using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
    public class Ore : IContainer
    {
        Texture2D texture;
        Vector2 position = Vector2.One * -100;
        int currentAmount;

        public Ore(int amount, Texture2D texture)
        {
            this.texture = texture;
            AddContent(amount);
        }

        public int CurrentAmount => currentAmount;

        public int MaxCapacity => 1000;

        public Vector2 Position { get; set; }

        public bool AddContent(int amount)
        {
            if (CurrentAmount + amount > MaxCapacity)
            {
                return false;
            }
            currentAmount += amount;
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }

}
