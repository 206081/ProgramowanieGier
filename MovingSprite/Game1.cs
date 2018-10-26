using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MovingSprite
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AnimateSprite runAnimate;
        private Vector2 position = new Vector2(100,100);
        private float speed = 3f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            runAnimate = new AnimateSprite();
            runAnimate.LoadGraphic(Content.Load<Texture2D>("run"), 4, 6, 100, 100, 6);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            runAnimate.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                position.Y -= speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                position.Y += speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                position.X -= speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                position.X += speed;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            runAnimate.Draw(spriteBatch, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
