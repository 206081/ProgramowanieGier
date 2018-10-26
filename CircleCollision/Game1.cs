using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

// This example simply adds a red rectangle to the screen
// then updates it's position along the X axis each frame.
namespace CircleCollision
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D textureRect, textureCirc;
        Vector2 positionRect, positionCirc;
        int radius;
    
        private Sprite circle, rectangle;
        private SpriteFont font;
        private double fromCenterOfCircle;

        private Vector2 rectTL;
        private Vector2 rectTR;
        private Vector2 rectBL;
        private Vector2 rectBR;
        private Vector2 circCenter;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            textureRect = Content.Load<Texture2D>("Box");
            textureCirc = Content.Load<Texture2D>("Box");

            base.Initialize();
        }

        protected override void LoadContent()
        {


            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureCirc = Content.Load<Texture2D>("Circle");
            positionCirc = new Vector2(50, 50);
            textureRect = Content.Load<Texture2D>("Box");
            positionRect = new Vector2(50, 50);
            font = Content.Load<SpriteFont>("PositionCirc");
            rectangle = new Sprite(textureRect)
            {
                position = positionRect,
                color = Color.Pink,
            };

            circle = new Sprite(textureCirc)
            {
                position = positionCirc,
                input = new Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                    Left = Keys.A,
                    Right = Keys.D,
                },
                color = Color.Red,
            };

            radius = textureCirc.Height / 2;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                Keys.Escape))
                Exit();

            circle.Update();

            rectTL = new Vector2(positionRect.X, positionRect.Y);
            rectTR = new Vector2(positionRect.X + textureRect.Width, positionRect.Y);
            rectBL = new Vector2(positionRect.X, positionRect.Y + textureRect.Height);
            rectBR = new Vector2(positionRect.X + textureRect.Width, positionRect.Y + textureRect.Height);
            circCenter = new Vector2(circle.position.X + textureCirc.Height / 2, circle.position.Y + textureCirc.Width / 2);

            if (circCenter.X > rectTL.X - radius & circCenter.X < rectTR.X + radius & circCenter.Y > rectTL.Y - radius & circCenter.Y < rectBR.Y + radius)
            {
                rectangle.color = Color.Blue;
                if (circCenter.X < rectTL.X & circCenter.Y < rectTL.Y)
                {
                    fromCenterOfCircle = Math.Sqrt(Math.Pow(circCenter.X - rectTL.X, 2) + Math.Pow(circCenter.Y - rectTL.Y, 2));
                    if (fromCenterOfCircle <= radius)
                        rectangle.color = Color.Blue;
                    else
                        rectangle.color = Color.Pink;

                }

                if (circCenter.X < rectBL.X & circCenter.Y > rectBL.Y)
                {
                    fromCenterOfCircle = Math.Sqrt(Math.Pow(circCenter.X - rectBL.X, 2) + Math.Pow(circCenter.Y - rectBL.Y, 2));
                    if (fromCenterOfCircle <= radius)
                        rectangle.color = Color.Blue;
                    else
                        rectangle.color = Color.Pink;
                }

                if (circCenter.X > rectBR.X & circCenter.Y > rectBR.Y)
                {
                    fromCenterOfCircle = Math.Sqrt(Math.Pow(circCenter.X - rectBR.X, 2) + Math.Pow(circCenter.Y - rectBR.Y, 2));
                    if (fromCenterOfCircle <= radius)
                        rectangle.color = Color.Blue;
                    else
                        rectangle.color = Color.Pink;
                }

                if (circCenter.X > rectTR.X & circCenter.Y < rectTR.Y)
                {
                    fromCenterOfCircle = Math.Sqrt(Math.Pow(circCenter.X - rectTR.X, 2) + Math.Pow(circCenter.Y - rectTR.Y, 2));
                    if (fromCenterOfCircle <= radius)
                        rectangle.color = Color.Blue;
                    else
                        rectangle.color = Color.Pink;
                }

            }
            else
                rectangle.color = Color.Pink;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            rectangle.Draw(spriteBatch);
            circle.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Position of center of circle" + circCenter.ToString(), new Vector2(100, 0), Color.Black);
            spriteBatch.DrawString(font, "Position of rect" + rectangle.position.ToString(), new Vector2(100, 15), Color.Black);
            spriteBatch.DrawString(font, "Distance of center to rect" + fromCenterOfCircle.ToString(), new Vector2(100, 30), Color.Black);
            


            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}