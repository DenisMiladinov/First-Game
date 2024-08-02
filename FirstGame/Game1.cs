using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using TiledSharp;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<Vector2, int> bg;
        private Dictionary<Vector2, int> fg;
        private TmxMap OWMap;
        private Texture2D textureAtlas;
        private Vector2 camera;
        private Rectangle playerStart;
        private List<Rectangle> colliders;
        private const int TILE_SIZE = 32;
        private List<Rectangle> intersections;
        private Texture2D whiteTexture;
        private Matrix matrix;

        private Texture2D caveSprite;
        private Texture2D characterSprite;
        private Texture2D fontSprite;
        private Texture2D InnerSprite;
        private Texture2D logSprite;
        private Texture2D NPC_testSprite;
        private Texture2D objectsSprite;
        private Texture2D OverworldSprite;
        private Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            fg = LoadMap("../../../Data/overworldMap_fg.csv");
            bg = LoadMap("../../../Data/overworldMap_bg.csv");
            camera = Vector2.Zero;
            intersections = new ();
        }
        

        protected override void Initialize()
        {
            Globals.WindowSize = Window.ClientBounds.Size;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureAtlas = Content.Load<Texture2D>("Sprites/Overworld");
            var map = new TmxMap("../../../Data/OWMap.tmx");
            colliders = new List<Rectangle>();

           foreach(var o in map.ObjectGroups["Collisions"].Objects)
           {
               if(o.Name == "")
               {
                   colliders.Add(new Rectangle((int)o.X * 2, (int)o.Y * 2, (int)o.Width * 2, (int)o.Height * 2));
               }
           }

            var spawnPoint = map.ObjectGroups["SpawnPoint"].Objects.First();

            player = new Player(Content.Load<Texture2D>("Sprites/character"));
            player.position = new Vector2((float)spawnPoint.X * 2, (float)spawnPoint.Y * 2);
            OWMap = map;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) camera.Y += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) camera.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) camera.X -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) camera.X += 5;

            Vector2 initialPos = player.position;

            player.Update();

            foreach(var o in colliders)
            {
                if (o.Intersects(player.playerRect))
                {
                    player.position = initialPos;
                }
            }

            var dx = (Globals.WindowSize.X / 2) - player.position.X;
            dx = MathHelper.Clamp(dx, -160, 0.0f);
            var dy = (Globals.WindowSize.Y / 2) - player.position.Y;
            dy = MathHelper.Clamp(dy, -160, 0.0f);
            matrix = Matrix.CreateTranslation(dx, dy, 0.0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix:matrix);

            int display_tilesize = 32;
            int num_tiles_per_row = 40;
            int pixel_tileSize = 16;

            foreach(var item in bg)
            {
                Rectangle drect = new(
                    (int)item.Key.X * display_tilesize,
                    (int)item.Key.Y * display_tilesize,
                    display_tilesize,
                    display_tilesize
                );

                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;

                Rectangle src = new(
                    x * pixel_tileSize,
                    y * pixel_tileSize,
                    pixel_tileSize,
                    pixel_tileSize
                );
                _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }

            foreach (var item in fg)
            {
                Rectangle drect = new(
                    (int)item.Key.X * display_tilesize,
                    (int)item.Key.Y * display_tilesize,
                    display_tilesize,
                    display_tilesize
                );

                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;

                Rectangle src = new(
                    x * pixel_tileSize,
                    y * pixel_tileSize,
                    pixel_tileSize,
                    pixel_tileSize
                );
                _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }

            player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for(int x = 0; x < items.Length; x++) 
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if(value > -1)
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
