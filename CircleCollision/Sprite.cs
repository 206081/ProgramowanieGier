using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CircleCollision
{
    class Sprite
    {
        private Texture2D texture;
        public Vector2 position;
        public float speed = 1f;
        public Input input;
        public Color color;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        
        public void Update()
        {
            Move();
        }

        private void Move()
        {

            if (input == null)
                return;

            if (Keyboard.GetState().IsKeyDown(input.Up))
            {
                position.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(input.Down))
            {
                position.Y += speed;
            }
            if (Keyboard.GetState().IsKeyDown(input.Left))
            {
                position.X -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(input.Right))
            {
                position.X += speed;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }

    }
}
