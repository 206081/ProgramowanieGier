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
    public class Sprite
    {

        protected Texture2D texture;
        public Vector2 position;

        public Vector2 velocity;
        protected int frameWidth;
        protected int frameHeight;

        public Sprite(Texture2D texture, Vector2 startPosition, GraphicsDevice graphicsDevice)
        {
            position = startPosition;
            this.texture = texture;
            frameHeight = texture.Height;
            frameWidth = texture.Width;
            updateBoundingBoxes();

        }

        protected BoundingBox boundingBox;
        public BoundingBox BoundingBox
        {
            get
            {
                return this.boundingBox;
            }

        }
        protected Rectangle boundingRectangle;
        public Rectangle BoundingRectangle
        {
            get
            {
                return this.boundingRectangle;
            }
        }

        #region BoundingBox
        protected BoundingBox bottomBoundingBox;
        public BoundingBox BottomBoundingBox
        {
            get
            {
                return this.bottomBoundingBox;
            }

        }
        protected BoundingBox topBoundingBox;
        public BoundingBox TopBoundingBox
        {
            get
            {
                return this.topBoundingBox;
            }

        }

        protected BoundingBox leftBoundingBox;
        public BoundingBox LeftBoundingBox
        {
            get
            {
                return this.leftBoundingBox;
            }

        }
        protected BoundingBox rightBoundingBox;
        public BoundingBox RightBoundingBox
        {
            get
            {
                return this.rightBoundingBox;
            }

        }
        #endregion


        protected void updateBoundingBoxes()
        {
            boundingBox = new BoundingBox(new Vector3(position.X, position.Y, 0), new Vector3(position.X + frameWidth, position.Y + frameHeight, 0));
            bottomBoundingBox = new BoundingBox(new Vector3(position.X + 4, position.Y + frameHeight - 4, 0), new Vector3(position.X + frameWidth - 4, position.Y + frameHeight, 0));
            topBoundingBox = new BoundingBox(new Vector3(position.X + 4, position.Y, 0), new Vector3(position.X + frameWidth - 4, position.Y + 4, 0));
            leftBoundingBox = new BoundingBox(new Vector3(position.X, position.Y + 4, 0), new Vector3(position.X + 4, position.Y + frameHeight - 4, 0));
            rightBoundingBox = new BoundingBox(new Vector3(position.X + frameWidth - 4, position.Y + 4, 0), new Vector3(position.X + frameWidth, position.Y + frameHeight - 4, 0));
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            }
        }

        public void Update(GameTime gameTime)
        {
            updateBoundingBoxes();
            updateRectengle();
        }

        private void updateRectengle()
        {
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }

        #region Collisions - BoundingBox
        public bool IsCollidingWith(Sprite otherSprite)
        {
            return this.boundingBox.Intersects(otherSprite.BoundingBox) ? true : false;
        }


               private void DrawRectangle(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, BoundingBox boundingBox, Color color)
        {

            int rectWidth = (int)(boundingBox.Max.X - boundingBox.Min.X);
            int rectHeight = (int)(boundingBox.Max.Y - boundingBox.Min.Y);

            Rectangle coords = new Rectangle((int)boundingBox.Min.X, (int)boundingBox.Min.Y, rectWidth, rectHeight);

        }

        #endregion
        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion
    }
}