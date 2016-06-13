using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DesignPatternsExercise
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _blank;
        SpriteFont font;
        ElementsFactory efac;
        TraditionalIterator<IElement> elements;
        List<IElement> elementsList;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _blank = new Texture2D(GraphicsDevice, 1, 1);

            elementsList = new List<IElement>();
            elements = new ElementsList<IElement>(elementsList);

            efac = new ElementsFactory();
            string msg;
            Action tmp = () => msg = "hello";
            var action = new Action(tmp);
            


            IElement aButton = efac.Create(1, _blank, action);
            IElement aButton2 = efac.Create(2, _blank, action);

            elementsList.Add(aButton);
            elementsList.Add(aButton2);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            _blank.SetData(new[] { Color.White });

            font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here

            IElementVisitor ev = new ElementVisitor();

            foreach (var item in elementsList)
            {
                item.Visit(ev);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            elements.MoveNext();
            //for (int i = 0; i < elementsList.Count; i++)
            //{
            //    spriteBatch.Draw(_blank, elements.Current.DrawRectangle, elements.Current.Color);
            //    elements.MoveNext();
            //}  

            foreach (var item in elementsList)
            {
                item.Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
