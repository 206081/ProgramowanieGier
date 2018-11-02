using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProgramowanieGier3.Models;
using System;
using System.Collections.Generic;

namespace ProgramowanieGier3
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playerTexture;
        Texture2D backgroundTexture;
        Texture2D groundTexture;
        Sprite groundSprite;
        ScrollingBackground scrollingBackground;
        Player animatePlayer;

        private List<Sprite> sprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 400;
            graphics.PreferredBackBufferWidth = 600;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            sprites = new List<Sprite>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("trump_run");
            backgroundTexture = Content.Load<Texture2D>("background");
            scrollingBackground = new ScrollingBackground(backgroundTexture, new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            groundTexture = Content.Load<Texture2D>("ground");
            groundSprite = new Sprite(groundTexture, new Vector2(0, graphics.GraphicsDevice.Viewport.Height - 50), GraphicsDevice);

            animatePlayer = new Player(playerTexture, Vector2.Zero, 4, 6, GraphicsDevice);
            


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            groundSprite.Update(gameTime);
            scrollingBackground.Update(gameTime);
            

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            animatePlayer.Update(gameTime);
            Console.WriteLine(animatePlayer.IsCollidingWith(groundSprite));
            
            foreach (var sprite in sprites)
            {
                animatePlayer.IsCollidingWith(sprite);
            }

            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            scrollingBackground.Draw(spriteBatch);

            groundSprite.Draw(GraphicsDevice, spriteBatch);

            foreach (var sprite in sprites)
            {
                sprite.Draw(GraphicsDevice, spriteBatch);
            }

            animatePlayer.Draw(GraphicsDevice, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
