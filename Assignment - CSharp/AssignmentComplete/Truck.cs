using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
    public class Truck : ITruck
    {
        private IContainer container;
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        

        public IContainer Container
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 Position
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 Velocity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddContainer(IContainer container)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void StartEngine()
        {
            throw new NotImplementedException();
        }

        public void Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
