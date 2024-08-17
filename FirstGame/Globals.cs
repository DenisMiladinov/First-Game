using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    public class Globals
    {
        public static float Time {  get; set; }
        public static ContentManager ContentManager { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static Point WindowSize { get; set; }
        public static float TotalSeconds { get; set; }

        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}