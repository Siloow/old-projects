using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{

  public class Product : IContainer
  {

    Texture2D texture;
    int currentAmount;
    Vector2 position = Vector2.One * -100;

    public Product(int amount, Texture2D texture)
    {
      this.texture = texture;
      AddContent(amount);
    }


    public int CurrentAmount
    {
      get
      {
        return currentAmount;
      }
    }

    public int MaxCapacity
    {
      get
      {
        return 100;
      }
    }

    public Vector2 Position
    {
      get
      {
        return position;
      }

      set
      {
        position = value;
      }
    }

    public bool AddContent(int amount)
    {
      if (CurrentAmount + amount > MaxCapacity)
      {
        Console.WriteLine("Too many...");
        return false;
      }
      currentAmount += amount;
      return true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero, Vector2.One * 0.2f, SpriteEffects.None, 0);      
    }

  }


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

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero, Vector2.One * 0.2f, SpriteEffects.None, 0);
    }

    public Vector2 Position
    {
      get
      {
        return position;
      }

      set
      {
        position = value;
      }
    }
    public int CurrentAmount
    {
      get
      {
        return currentAmount;
      }
    }

    public int MaxCapacity
    {
      get
      {
        return 1000;
      }
    }

    public bool AddContent(int amount)
    {
      if (CurrentAmount + amount > MaxCapacity)
      {
        Console.WriteLine("Too many...");
        return false;
      }
      currentAmount += amount;
      return true;
    }
  }
}
