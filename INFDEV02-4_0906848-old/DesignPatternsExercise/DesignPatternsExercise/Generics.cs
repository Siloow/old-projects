using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternsExercise
{
    interface IOptionVisitor<T, U>
    {
        U onSome(T value);
        U onNone();
    } 

    interface IOption<T>
    {
        U Visit<U>(IOptionVisitor<T, U> visitor);
    }
    class Some<T> : IOption<T>
    {
        public T value;
        public Some(T value) { this.value = value; }
        public U Visit<U>(IOptionVisitor<T, U> visitor)
        {
            return visitor.onSome(this.value);
        }
    }
    class None<T> : IOption<T>
    {
        public U Visit<U>(IOptionVisitor<T, U> visistor)
        {
            return visistor.onNone();
        }
    }

    public interface IElementVisitor
    {
        void onButton(Button button);
        void onLabel(Label label);
    }
    class ElementVisitor : IElementVisitor
    {
        public void onButton(Button button)
        {
            MouseState input = Mouse.GetState();
            Point mousePosition = new Point(input.X, input.Y);
            if (button.DrawRectangle.Contains(mousePosition) && input.LeftButton == ButtonState.Pressed)
            {

                Console.WriteLine("clicked");
                // change color of button or text?
            }
        }
        public void onLabel(Label label)
        {

        }
    }
    
}
