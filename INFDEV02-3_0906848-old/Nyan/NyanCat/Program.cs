using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanCat
{
    class Game : Microsoft.Xna.Framework.Game
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        NyanCat.GameState.GameState gameState;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameState = NyanCat.GameState.initialState();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            gameState = NyanCat.GameState.updateShip(
                Keyboard.Get)    
            base.Update(gameTime);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
