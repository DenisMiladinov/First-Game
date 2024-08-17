using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    public class Animation
    {
        private readonly Texture2D _texture;
        private readonly List<Rectangle> _sourceRectangle = new();
        private readonly int _frames;
        private int _frame;
        private readonly float _frameTime;
        private float _frameTimeLeft;
        private bool _active = true;

        public Animation(Texture2D texture, int framesX, float frameTime, Point bounds, Point offset)
        {
            _texture = texture;
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _frames = framesX;

            var frameWidth = bounds.X;
            var frameHeight = bounds.Y;

            for (int i = 0; i < _frames; i++)
            {
                _sourceRectangle.Add(new(i * frameWidth + offset.X, offset.Y, frameWidth, frameHeight));
            }
        }

        public void Start()
        {
            _active = true;
        }

        public void Stop() 
        {
            _active = false;
        }

        public void Reset()
        {
            _frame = 0;
            _frameTimeLeft = _frameTime;
        }

        public void Update() 
        {
            if (!_active) return;

            _frameTimeLeft -= Globals.TotalSeconds;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _frames;
            }
        }

        public void Draw(Vector2 pos)
        {
            Globals.SpriteBatch.Draw(_texture, pos, _sourceRectangle[_frame], Color.White, 0, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 1);
        }
    }
}