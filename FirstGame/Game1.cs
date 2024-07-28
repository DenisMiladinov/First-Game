using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
        }
        

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            caveSprite = Content.Load<Texture2D>("Sprites/cave");
            //characterSprite = Content.Load<Texture2D>("Sprites/character");
            InnerSprite = Content.Load<Texture2D>("Sprites/Inner");
            logSprite = Content.Load<Texture2D>("Sprites/log");
            NPC_testSprite = Content.Load<Texture2D>("Sprites/NPC_test");
            objectsSprite = Content.Load<Texture2D>("Sprites/objects");
            OverworldSprite = Content.Load<Texture2D>("Sprites/Overworld");

            player = new Player(Content.Load<Texture2D>("Sprites/character"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Rectangle sourceRectangle = new Rectangle(0, 0, 15, 15);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(0, 0, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(200, 0, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(0, 200, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(200, 200, 200, 200), sourceRectangle, Color.White);
            /*_spriteBatch.Draw(OverworldSprite, new Rectangle(400, 0, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(400, 200, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(400, 400, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(0, 400, 200, 200), sourceRectangle, Color.White);
            _spriteBatch.Draw(OverworldSprite, new Rectangle(200, 400, 200, 200), sourceRectangle, Color.White);*/

            player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
