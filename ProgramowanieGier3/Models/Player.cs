using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramowanieGier3.Models
{

    enum WalkingDirection
    {
        up = 2,
        left = 3,
        down = 0,
        right = 1,
    }

    public class Player : Sprite
    {
        int numberOfAnimationRows = 4;
        int animationFramesInRow = 6;

        int whichFrame;
        double currentFrameTime = 0;
        double expectedFrameTime = 200.0f;
        WalkingDirection currentWalkingDirection = WalkingDirection.down;

        public bool isFalling = true;
        private bool isTouchingLeft = false;
        private bool isTouchingRight = false;

        float gravity = 0.15f;
        float jumpStrength = 6f;
        Vector2 momentum = Vector2.Zero;

        public Player(Texture2D texture, Vector2 startingPosition, int numberOfAnimationRows, int animationFramesInRow, GraphicsDevice graphicsDevice) : base(texture, startingPosition, graphicsDevice)
        {
            base.frameHeight = texture.Height / numberOfAnimationRows;
            base.frameWidth = texture.Width / animationFramesInRow;

            this.numberOfAnimationRows = numberOfAnimationRows;
            this.animationFramesInRow = animationFramesInRow;
            boundingBox = new BoundingBox(new Vector3(position.X, position.Y, 0), new Vector3(position.X + frameWidth, position.Y + frameHeight, 0));

        }

        new public void Update(GameTime gameTime)
        {
            currentFrameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currentFrameTime >= expectedFrameTime)
            {
                whichFrame = (whichFrame < animationFramesInRow - 1) ? whichFrame + 1 : 0;
                currentFrameTime = 0;
            }

            updateMovement(gameTime);
            ApplyGravity();
            isFalling = true;
            base.updateBoundingBoxes();
        }


        void updateMovement(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var pressedKeys = keyboardState.GetPressedKeys();

            int pixelsPerSecond = 100;
            float movementSpeed = (float)(pixelsPerSecond * (gameTime.ElapsedGameTime.TotalSeconds));

            Vector2 movementVector = Vector2.Zero;
            if (pressedKeys.Length == 0)
            {
                whichFrame = 0;
                //currentWalkingDirection = WalkingDirection.down;
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (!isTouchingLeft) movementVector += new Vector2(-movementSpeed, 0);
                    currentWalkingDirection = WalkingDirection.left;
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (!isTouchingRight) movementVector += new Vector2(movementSpeed, 0);
                    currentWalkingDirection = WalkingDirection.right;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Jump();

                position += movementVector;

            }
        }

        public void Jump()
        {
            if (isFalling == false)
            {
                momentum = new Vector2(0, -jumpStrength);
                isFalling = true;
            }

        }

        public void ApplyGravity()
        {
            if (isFalling)
            {
                momentum.Y += gravity;
            }

            else
            {
                momentum.Y = 0;
            }

            position += momentum;
        }

        new public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(whichFrame * base.frameWidth, base.frameHeight * (int)currentWalkingDirection, base.frameWidth, base.frameHeight), Color.White);
        }

        new public bool IsCollidingWith(Sprite sprite)
        {
            //if (this.velocity.X > 0 && this.IsTouchingLeft(sprite))
            //    isTouchingLeft = true;
            //else
            //    isTouchingLeft = false;

            //if (this.velocity.X < 0 & this.IsTouchingRight(sprite))
            //    isTouchingRight = true;
            //else
            //    isTouchingRight = false;

            //if (this.velocity.Y > 0 && this.IsTouchingTop(sprite))
            //    isFalling = false;
            //else
            //    isFalling = true;

            if (this.bottomBoundingBox.Intersects(sprite.TopBoundingBox))
            {
                isFalling = false;
            }
            
            if (this.leftBoundingBox.Intersects(sprite.RightBoundingBox))
            {
                isTouchingLeft = true;
            }
            else isTouchingLeft = false;
            if (this.rightBoundingBox.Intersects(sprite.LeftBoundingBox))
            {
                isTouchingRight = true;
            }
            else isTouchingRight = false;

            return this.boundingBox.Intersects(sprite.BoundingBox) ? true : false;

        }

    }
}
