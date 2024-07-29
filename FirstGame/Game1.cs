using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<Vector2, int> bg;
        private Dictionary<Vector2, int> fg;
        private Texture2D textureAtlas;
        private Vector2 camera;

        Texture2D caveSprite;
        Texture2D characterSprite;
        Texture2D fontSprite;
        Texture2D InnerSprite;
        Texture2D logSprite;
        Texture2D NPC_testSprite;
        Texture2D objectsSprite;
        Texture2D OverworldSprite;
        Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            fg = LoadMap("../../../Data/overworldMap_fg.csv");
            bg = LoadMap("../../../Data/overworldMap_bg.csv");
            camera = Vector2.Zero;
        }
        

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /*caveSprite = Content.Load<Texture2D>("Sprites/cave");
            //characterSprite = Content.Load<Texture2D>("Sprites/character");
            InnerSprite = Content.Load<Texture2D>("Sprites/Inner");
            logSprite = Content.Load<Texture2D>("Sprites/log");
            NPC_testSprite = Content.Load<Texture2D>("Sprites/NPC_test");
            objectsSprite = Content.Load<Texture2D>("Sprites/objects");
            OverworldSprite = Content.Load<Texture2D>("Sprites/Overworld");*/

            textureAtlas = Content.Load<Texture2D>("Sprites/Overworld");

            player = new Player(Content.Load<Texture2D>("Sprites/character"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                camera.Y += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                camera.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                camera.X -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                camera.X += 5;

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            /*Rectangle sourceRectangle = new Rectangle(0, 0, 15, 15);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(0, 0, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(200, 0, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(0, 200, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(200, 200, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(400, 0, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(400, 200, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(400, 400, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(0, 400, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(200, 400, 200, 200), sourceRectangle, Color.White);*/

            int display_tilesize = 32;
            int num_tiles_per_row = 40;
            int pixel_tileSize = 16;

            foreach(var item in bg)
            {
                Rectangle drect = new(
                    (int)item.Key.X * display_tilesize + (int)camera.X,
                    (int)item.Key.Y * display_tilesize + (int)camera.Y,
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
                    (int)item.Key.X * display_tilesize + (int)camera.X,
                    (int)item.Key.Y * display_tilesize + (int)camera.Y,
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
