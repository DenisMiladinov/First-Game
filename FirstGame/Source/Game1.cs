using FirstGame.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            Globals.WindowSize = Window.ClientBounds.Size;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.ContentManager = Content;

            LevelManager.AddLevel(nameof(OverworldLevel), new OverworldLevel());
            LevelManager.SetCurrent(nameof(OverworldLevel));
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            LevelManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            LevelManager.Draw();

            base.Draw(gameTime);
        }
    }
}
