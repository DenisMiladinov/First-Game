using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace FirstGame
{
    internal class Level
    {
        public virtual void LoadContent() { }
        public virtual void Update() { }
        public virtual void Draw() { }


        protected Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }

                y++;
            }
            return result;
        }
    }
}
