using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position {  get; protected set; }
        public Vector2 origin { get; protected set; }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.origin = new(texture.Width / 2, texture.Height / 2);
        }
    }
}
