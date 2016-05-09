﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
  public interface IDrawable
  {
    void Draw(SpriteBatch spriteBatch);
  }
  public interface IUpdateable
  {
    void Update(float dt);
  }
  
  // An interface is missing. Write it.
  // TODO: 2 points
  public interface IComponent : IDrawable, IUpdateable
    {

    }
}
