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

        
        private class AddOreContainerToTruck : IAction
        {
            private Mine mine;

            public AddOreContainerToTruck(Mine mine)
            {
                this.mine = mine;
            }

            public void Run()
            {
                Console.WriteLine("callled");
                
            }
        }

        private class RemoveTruckFromMine : IAction
        {
            private Mine mine;

            public RemoveTruckFromMine(Mine mine)
            {
                this.mine = mine;
            }

            public void Run()
            {
                Console.WriteLine("remTruck");
            }
        }

        private class AddEmptyTruckToMine : IAction
        {
            private Mine mine;

            public AddEmptyTruckToMine(Mine mine)
            {
                this.mine = mine;
            }

            public void Run()
            {
                Console.WriteLine("addEmpty");
            }
        }

        Texture2D mine, oreContainer, mineCart, truckTexure;
        Vector2 position;
        ITruck Truck;

        List<IContainer> productsToShip, products;
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

            processes.Add(new Repeat(new Seq(new Wait(() => ProductsToShip.Count > 3),
                            new Seq(new Call(new AddOreContainerToTruck(this)),
                                new Seq(new Call(new RemoveTruckFromMine(this)),
                                    new Seq(new Timer(1.0f),
                                        new Call(new AddEmptyTruckToMine(this))))))));

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
            ITruck departing = this.Truck;
            IContainer ore_container = this.ProductsToShip.First();

            ore_container.AddContent(ore_container.MaxCapacity - ore_container.CurrentAmount);

            departing.AddContainer(ore_container);

            if (this.Truck == null)
            {
                return new Truck(new Vector2(200, 200), truckTexure, oreContainer);
            }
            return null;
       
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

    class Ikea : IFactory
    {
        class AddOreBoxToMine : IAction
        {
            Ikea ikea;
            public AddOreBoxToMine(Ikea ikea)
            {
                this.ikea = ikea;
            }
            public void Run()
            {
                ikea.productsToShip.Add(CreateMineCart(ikea.Position + new Vector2(120, 40 + -30 * ikea.ProductsToShip.Count)));
            }
            Ore CreateMineCart(Vector2 position)
            {
                var box = new Ore(100, ikea.productCart);
                box.Position = position;
                return box;
            }
        }

        Texture2D ikea, productContainer, productCart, truckTexure;
        Vector2 position;

        List<IContainer> productsToShip;
        List<IStateMachine> processes;

        public Ikea(Vector2 position, Texture2D truck_tex, Texture2D ikea, Texture2D product_cart, Texture2D product_container)
        {
            processes = new List<IStateMachine>();
            productsToShip = new List<IContainer>();
            this.position = position;
            this.truckTexure = truck_tex;
            this.ikea = ikea;
            this.productCart = product_cart;
            this.productContainer = product_container;

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
            spriteBatch.Draw(ikea, Position, Color.White);

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
