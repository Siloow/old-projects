using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DesignPatternsExercise
{
    public interface IElement
    {
        Point Location { get; }
        Point Size { get; }
        Color Color { get; }
        Rectangle DrawRectangle { get; }
    }
}
