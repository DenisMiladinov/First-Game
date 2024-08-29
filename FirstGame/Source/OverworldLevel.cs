using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

namespace FirstGame
{
    internal class OverworldLevel : Level
    {
        private Player _player;
        private TmxMap _map;
        private Dictionary<Vector2, int> _background;
        private Dictionary<Vector2, int> _foreground;
        private Texture2D _textureAtlas;
        private List<Rectangle> _colliders;
        private Camera _camera;

        public override void LoadContent()
        {
            _foreground = LoadMap("../../../Data/overworldMap_fg.csv");
            _background = LoadMap("../../../Data/overworldMap_bg.csv");

            _textureAtlas = Globals.ContentManager.Load<Texture2D>("Sprites/Overworld");
            _map = new TmxMap("../../../Data/OWMap.tmx");
            _colliders = new List<Rectangle>();

            foreach (var o in _map.ObjectGroups["Collisions"].Objects)
            {
                if (o.Name == "")
                {
                    _colliders.Add(new Rectangle((int)o.X * 2, (int)o.Y * 2, (int)o.Width * 2, (int)o.Height * 2));
                }
            }

            var spawnPoint = _map.ObjectGroups["SpawnPoint"].Objects.First();

            _player = new Player(Globals.ContentManager.Load<Texture2D>("Sprites/character"));
            _player.position = new Vector2((float)spawnPoint.X * 2, (float)spawnPoint.Y * 2);
            _camera = new Camera();
        }


        public override void Update()
        {
            Vector2 initialPos = _player.position;

            _player.Update();

            foreach (var o in _colliders)
            {
                if (o.Intersects(_player.playerRect))
                {
                    _player.position = initialPos;
                }
            }

            var dx = (Globals.WindowSize.X / 2) - _player.position.X;
            dx = MathHelper.Clamp(dx, -160, 0.0f);
            var dy = (Globals.WindowSize.Y / 2) - _player.position.Y;
            dy = MathHelper.Clamp(dy, -160, 0.0f);
            _camera.FollowTarget(dx, dy);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.ProjectionViewMatrix);

            int display_tilesize = 32;
            int num_tiles_per_row = 40;
            int pixel_tileSize = 16;

            foreach (var item in _background)
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
                Globals.SpriteBatch.Draw(_textureAtlas, drect, src, Color.White);
            }

            foreach (var item in _foreground)
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
                Globals.SpriteBatch.Draw(_textureAtlas, drect, src, Color.White);
            }

            _player.Draw(Globals.SpriteBatch);
            Globals.SpriteBatch.End();
        }
    }
}
