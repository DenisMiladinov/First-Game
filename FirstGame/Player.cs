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

        public Player(Texture2D texture)
        {
            this.texture = texture;
            speed = 5;
            playerSize = new Vector2(32, 56);
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W)) velocity.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S)) velocity.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A)) velocity.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D)) velocity.X += 1;
            
            if(velocity != Vector2.Zero) 
            { 
                velocity.Normalize();
            }

            position += velocity * speed;
            velocity = Vector2.Zero;

            playerRect = new Rectangle(position.ToPoint() + new Point(0, 32), new Point(32, 24));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), playerSize.ToPoint()), new Rectangle(0, 0, 16, 28), Color.White);
        }
    }
}
