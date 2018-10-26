using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingSprite
{
    public class AnimateSprite
    {
        public Texture2D Texture;     // texture

        private float totalElapsed;   // elapsed time

        private int rows;             // number of rows
        private int columns;          // number of columns
        private int width;            // width of a graphic
        private int height;           // height of a graphic
        private float animationSpeed; // pictures per second

        private int currentRow;       // current row
        private int currentColumn;    // current culmn

        public void LoadGraphic(
          Texture2D texture,
          int rows,
          int columns,
          int width,
          int height,
          int animationSpeed
          )
            {
                this.Texture = texture;
                this.rows = rows;
                this.columns = columns;
                this.width = width;
                this.height = height;
                this.animationSpeed = (float)1 / animationSpeed;

                totalElapsed = 0;
                currentRow = 0;
                currentColumn = 0;
            }

        public void Update(float elapsed)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                currentRow = 2;
                Move(elapsed);
            }
                
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                currentRow = 0;
                Move(elapsed);
            }
                
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                currentRow = 3;
                Move(elapsed);

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                currentRow = 1;
                Move(elapsed);
            }
            else
            {
                currentRow = 0;
                Move(0);
            }
               
        }

        private void Move(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > animationSpeed)
            {
                totalElapsed -= animationSpeed;
                currentColumn += 1;

                if (currentColumn >= columns)
                     currentColumn = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(
                Texture,
                new Rectangle((int)position.X, (int)position.Y, width, height),
                new Rectangle(
                  currentColumn * width,
                  currentRow * height,
                  width, height),
                color
                );
        }
    }
}
