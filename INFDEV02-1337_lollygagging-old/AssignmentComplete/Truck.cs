using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
  class Volvo : ITruck
  {
    Texture2D truck;
    public Volvo(Texture2D texture, Vector2 position, Vector2 velocity)
    {
      Velocity = velocity;
      Position = position;
      this.truck = texture;
    }
    public void AddContainer(IContainer container)
    {
      this.container = container;
    }
    IContainer container;
    public IContainer Container
    {
      get
      {
        return container;
      }
    }

    Vector2 position;
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
    Vector2 velocity;
    public Vector2 Velocity
    {
      get
      {
        return velocity;
      }
      set
      {
        velocity = value;
      }
    }
    bool startEngine = false;
    public void StartEngine()
    {
      startEngine = true;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
      SpriteEffects effect = SpriteEffects.None;

      if (Velocity.X < 0)
        effect = SpriteEffects.FlipHorizontally;
      spriteBatch.Draw(truck, Position, null, Color.White, 0, Vector2.Zero, Vector2.One * 0.2f, effect, 0);

      if (Container != null)
        Container.Draw(spriteBatch);
    }

    public void Update(float dt)
    {

      if (Container != null)
      {
        Vector2 containerPosition = new Vector2(0, -8);
        if (Velocity.X < 0)
          containerPosition = new Vector2(40, -15);
        Container.Position = Position + containerPosition;
        if(startEngine)
          Position = Position + Velocity * dt;

      }
    }
  }
}
