using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    public class Player
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public float speed;
        public Rectangle playerRect;
        private Vector2 playerSize; 
        private Animation AnimationDown;
        private Animation AnimationUp;
        private Animation AnimationLeft;
        private Animation AnimationRight;
        private enum Direction { Left, Right, Up, Down }
        Direction direction = Direction.Down;

        public Player(Texture2D texture)
        {
            this.texture = texture;
            speed = 5;
            playerSize = new Vector2(32, 56);
            AnimationDown = new Animation(texture, 4, 0.1f, new Point(16, 28), new Point(0, 0));
            AnimationRight = new Animation(texture, 4, 0.1f, new Point(16, 28), new Point(0, 32));
            AnimationUp = new Animation(texture, 4, 0.1f, new Point(16, 28), new Point(0, 64));
            AnimationLeft = new Animation(texture, 4, 0.1f, new Point(16, 28), new Point(0, 96));
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            velocity = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W)) velocity.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S)) velocity.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A)) velocity.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D)) velocity.X += 1;
            
            if(velocity != Vector2.Zero) 
            { 
                velocity.Normalize();
            }

            position += velocity * speed;

            playerRect = new Rectangle(position.ToPoint() + new Point(0, 32), new Point(32, 24));

            if (velocity.X >= 1) 
            {
                AnimationRight.Start();
                direction = Direction.Right;
            }
            else if (velocity.X <= -1)
            {
                AnimationLeft.Start();
                direction = Direction.Left;
            }
            else if (velocity.Y >= 1) 
            {
                AnimationDown.Start();
                direction = Direction.Down;
            }
            else if (velocity.Y <= -1) 
            {
                AnimationUp.Start();
                direction = Direction.Up;
            }
            else
            {
                AnimationRight.Stop();
                AnimationRight.Reset();

                AnimationLeft.Stop();
                AnimationLeft.Reset();

                AnimationDown.Stop();
                AnimationDown.Reset();

                AnimationUp.Stop();
                AnimationUp.Reset();
            }
            AnimationRight.Update();
            AnimationLeft.Update();
            AnimationDown.Update();
            AnimationUp.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), playerSize.ToPoint()), new Rectangle(0, 0, 16, 28), Color.White);
            switch (direction) 
            {
                case Direction.Right:
                    AnimationRight.Draw(position);
                    break;
                case Direction.Left:
                    AnimationLeft.Draw(position);
                    break;
                case Direction.Up:
                    AnimationUp.Draw(position);
                    break;
                case Direction.Down:
                    AnimationDown.Draw(position);
                    break;
            }
        }
    }
}