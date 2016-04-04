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
        private Texture2D container_texture;
        public Boolean reverse;

        public Truck(Vector2 pos, Texture2D text_truck, Texture2D text_cont)
        {
            this.position = pos;
            this.texture = text_truck;
            this.container_texture = text_cont;
        }

        public IContainer Container
        {
            get
            {
                return this.container;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public void AddContainer(IContainer container)
        {
            this.container = container;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            if (this.container != null)
            {
                spriteBatch.Draw(this.container_texture, position, Color.White);
            }
        }

        public void StartEngine()
        {
            this.velocity = new Vector2(10, 0);
        }

        public void Update(float dt)
        {
            if (this.container != null)
            {
                this.position.X = this.position.X + this.velocity.X * dt;
            }
        }
    }
}
