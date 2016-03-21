using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
    class Mine : IFactory
    {
        class AddOreBoxToMine : IAction
        {
            Mine mine;
            public AddOreBoxToMine(Mine mine)
            {
                this.mine = mine;
            }
            public void Run()
            {
                mine.productsToShip.Add(CreateMineCart(mine.Position + new Vector2(-80, 40 + -30 * mine.ProductsToShip.Count)));
            }
            Ore CreateMineCart(Vector2 position)
            {
                var box = new Ore(100, mine.mineCart);
                box.Position = position;
                return box;
            }
        }

        Texture2D mine, oreContainer, mineCart, truckTexure;
        Vector2 position;

        List<IContainer> productsToShip;
        List<IStateMachine> processes;

        public Mine(Vector2 position, Texture2D truck_tex, Texture2D mine, Texture2D mine_cart, Texture2D ore_container)
        {
            processes = new List<IStateMachine>();
            productsToShip = new List<IContainer>();
            this.position = position;
            this.truckTexure = truck_tex;
            this.mine = mine;
            this.mineCart = mine_cart;
            this.oreContainer = ore_container;

            processes.Add(new Repeat(new Seq(new Timer(1.0f), new Call(new AddOreBoxToMine(this)))));

        }

        public Vector2 Position => position;

        public List<IContainer> ProductsToShip
        {
            get
            {
                return productsToShip;
            }
            set
            {
                productsToShip = value;
            }
        }


        public ITruck GetReadyTruck()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var cart in ProductsToShip)
            {
                cart.Draw(spriteBatch);
            }
            spriteBatch.Draw(mine, Position, Color.White);

        }

        public void Update(float dt)
        {
            foreach (var process in processes)
            {
                process.Update(dt);
            }
        }
    }
}
