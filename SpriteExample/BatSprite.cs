using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpriteExample
{
    public enum Direction
    {

        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3        
    }

    /// <summary>
    /// the bat sprite
    /// </summary>
    public class BatSprite
    {

        private Texture2D texture;

        private double directionTimer;

        private double animationTimer;

        private short animationFrame = 1;

        /// <summary>
        /// the direction of the bat
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// the position of the bat
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// loads the bat sprite texture
        /// </summary>
        /// <param name="content"> the content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("32x32-bat-sprite");
        }

        /// <summary>
        /// Updates the bat sprite to fly in a  pattern
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //update the direction timer
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //stitch directions every two seconds
            if(directionTimer > 2.0)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Direction = Direction.Down;
                        break;
                    case Direction.Down:
                        Direction = Direction.Right;
                        break;
                    case Direction.Right:
                        Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        Direction = Direction.Up;
                        break;
                }
                directionTimer -= 2.0;
            }

            //Move the bat in the dirction it is flying
            switch (Direction) 
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }


        }

        /// <summary>
        /// Draws the animated bat sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch"> the spritebatech to draw</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if(animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 1;
                animationTimer -= 0.3;
            }

            // Draw Sprite
            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }


    }
}
