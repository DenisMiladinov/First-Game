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

        public Player(Texture2D texture)
        {
            this.texture = texture;
            speed = 5;
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, 15, 27), Color.White);
        }
    }
}
