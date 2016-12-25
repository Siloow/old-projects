using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
  class AddOreBoxToMine : IAction
  {
    MiningFactory mine;
    public AddOreBoxToMine(MiningFactory mine)
    {
      this.mine = mine;
    }
    public void Run()
    {
      mine.ProductsToShip.Add(CreateOreBox(mine.Position + new Vector2(-80, 40 + -30 * mine.ProductsToShip.Count)));
    }
    Ore CreateOreBox(Vector2 position)
    {
      var box = new Ore(100, mine.oreBox);
      box.Position = position;
      return box;
    }
    }

  // TODO: UNCOMMENT THE COMMENTED LINES AND COMPLETE THE MISSING LINES
  // POINTS 4
  class AddOreContainerToTruck : IAction
    {
    MiningFactory mine;
    public AddOreContainerToTruck(MiningFactory mine)
    {
        this.mine = mine;
    }
    public void Run()
    {
        mine.waitingTruck.AddContainer(new Ore(compute_amounts(3), mine.oreContainer));
        mine.ProductsToShip.RemoveRange(0, 3);
        mine.isTruckReady = true;
    }
    int compute_amounts(int max)
    {
        int sum = 0;
        for (int i = 0; i < max; i++)
        {
            sum = mine.ProductsToShip[i].CurrentAmount;
        }
        return sum;
    }
}
class RemoveTruckFromMine : IAction
  {
    MiningFactory mine;
    public RemoveTruckFromMine(MiningFactory mine)
    {
      this.mine = mine;
    }

    public void Run()
    {
      mine.waitingTruck = null;
    }
  }
  class AddEmptyTruckToMine : IAction
  {
    MiningFactory mine;
    public AddEmptyTruckToMine(MiningFactory mine)
    {
      this.mine = mine;
    }

    public void Run()
    {
      mine.waitingTruck = new Volvo(mine.truckTexture, mine.Position + Vector2.UnitX * 120, Vector2.UnitX * 100);
    }
  }

  class MiningFactory : IFactory
  {
    public Texture2D mine, oreContainer, oreBox, truckTexture;
    public List<IStateMachine> processes;
    public ITruck waitingTruck;
    public bool isTruckReady = false;
    public Vector2 position;
    public List<IContainer> productsToShip;

    public MiningFactory(Vector2 position, Texture2D truck_texture, Texture2D mine, Texture2D ore_box, Texture2D ore_container)
    {
      processes = new List<IStateMachine>();
      ProductsToShip = new List<IContainer>();
      this.mine = mine;
      this.truckTexture = truck_texture;
      this.oreContainer = ore_container;
      this.oreBox = ore_box;
      this.position = position;

      new AddEmptyTruckToMine(this).Run();

      processes.Add(
        new Repeat(new Seq(new Timer(1.0f),
                           new Call(new AddOreBoxToMine(this)))));
        //TODO: DECOMMENT WHEN ALL CLASSES CORRECTLY FILLED IN
        processes.Add(
          new Repeat(new Seq(new Wait(() => ProductsToShip.Count > 3),
                     new Seq(new Call(new AddOreContainerToTruck(this)),
                     new Seq(new Call(new RemoveTruckFromMine(this)),
                     new Seq(new Timer(1.0f),
                             new Call(new AddEmptyTruckToMine(this))))))));
    }


    public ITruck GetReadyTruck()
    {
      if (isTruckReady == true)
      {
        isTruckReady = false;
        return waitingTruck;
      }
      else return null;
    }

    public Vector2 Position
    {
      get
      {
        return position;
      }
    }
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
    public void Draw(SpriteBatch spriteBatch)
    {
      var scale = 0.6f;
      var width = (float)mine.Width * scale;
      var height = (float)mine.Height * scale;
      foreach (var cart in ProductsToShip)
      {
        cart.Draw(spriteBatch);
      }
      if (waitingTruck != null)
        waitingTruck.Draw(spriteBatch);
      spriteBatch.Draw(mine, Position, null, Color.White, 0, new Vector2(width / 2, height / 2), Vector2.One * scale, SpriteEffects.None, 0);
    }
    public void Update(float dt)
    {
      foreach (var process in processes)
      {
        process.Update(dt);
      }
    }

  }

  class AddProductBoxToIkea : IAction
  {
    IkeaFactory ikea;
    public AddProductBoxToIkea(IkeaFactory ikea)
    {
      this.ikea = ikea;
    }
    public void Run()
    {
      ikea.ProductsToShip.Add(CreateProductBox(ikea.Position + new Vector2(120, 40 + -30 * ikea.ProductsToShip.Count)));
    }
    Product CreateProductBox(Vector2 position)
    {
      var box = new Product(100, ikea.productBox);
      box.Position = position;
      return box;
    }
    }
  
  // TODO: UNCOMMENT THE COMMENTED LINES AND COMPLETE THE MISSING LINES
  // POINTS 4
  class AddProductContainerToTruck : IAction
    {
    IkeaFactory ikea;

    public AddProductContainerToTruck(IkeaFactory ikea)
        {
            this.ikea = ikea;
        }
        public void Run()
    {
        ikea.waitingTruck.AddContainer(new Ore(compute_amounts(3), ikea.productContainer));
        ikea.ProductsToShip.RemoveRange(0, 3);
        ikea.isTruckReady = true;
    }
    int compute_amounts(int max)
    {
        int sum = 0;
        for (int i = 0; i < max; i++)
        {
            sum = ikea.ProductsToShip[i].CurrentAmount;
        }
        return sum;
    }
}
class RemoveTruckFromIkea : IAction
  {
    IkeaFactory ikea;
    public RemoveTruckFromIkea(IkeaFactory ikea)
    {
      this.ikea = ikea;
    }

    public void Run()
    {
      ikea.waitingTruck = null;
    }
  }
  class AddTruckToIkea : IAction
  {
    IkeaFactory ikea;
    public AddTruckToIkea(IkeaFactory ikea)
    {
      this.ikea = ikea;
    }

    public void Run()
    {
      ikea.waitingTruck = new Volvo(ikea.truckTexture, ikea.Position + Vector2.UnitX * -160, Vector2.UnitX * -80);
    }
  }


  class IkeaFactory : IFactory
  {

    public List<IStateMachine> processes;
    public ITruck waitingTruck;
    public bool isTruckReady = false;
    public Texture2D ikea, productContainer, productBox, truckTexture;
    public Vector2 position;
    public List<IContainer> productsToShip;

    public IkeaFactory(Vector2 position, Texture2D truck_texture, Texture2D ikea, Texture2D product_box, Texture2D product_container)
    {
      this.truckTexture = truck_texture;
      processes = new List<IStateMachine>();
      ProductsToShip = new List<IContainer>();
      this.ikea = ikea;
      this.productContainer = product_container;
      this.productBox = product_box;
      this.position = position;

      waitingTruck = new Volvo(truck_texture, Position + Vector2.UnitX * -160, Vector2.UnitX * -80);

      processes.Add(
                    new Repeat(new Seq(new Timer(1.0f),
                                       new Call(new AddProductBoxToIkea(this)))));
        //TODO: DECOMMENT WHEN ALL CLASSES CORRECTLY FILLED IN
        processes.Add(
          new Repeat(new Seq(new Wait(() => ProductsToShip.Count > 3),
                             new Seq(new Call(new AddProductContainerToTruck(this)),
                             new Seq(new Call(new RemoveTruckFromIkea(this)),
                             new Seq(new Timer(1.0f),
                             new Call(new AddTruckToIkea(this))))))));


    }

    public ITruck GetReadyTruck()
    {
      if (isTruckReady == true)
      {
        isTruckReady = false;
        return waitingTruck;
      }
      else return null;
    }


    public Vector2 Position
    {
      get
      {
        return position;
      }
    }
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


    public void Draw(SpriteBatch spriteBatch)
    {
      var scale = 0.6f;
      var width = (float)ikea.Width * scale;
      var height = (float)ikea.Height * scale;
      foreach (var box in ProductsToShip)
      {
        box.Draw(spriteBatch);
      }
      if (waitingTruck != null)
        waitingTruck.Draw(spriteBatch);

      spriteBatch.Draw(ikea, Position, null, Color.White, 0, new Vector2(width / 2, height / 2), Vector2.One * scale, SpriteEffects.None, 0);
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
