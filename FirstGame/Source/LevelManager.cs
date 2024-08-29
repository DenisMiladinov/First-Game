using System.Collections.Generic;

namespace FirstGame.Source
{
    internal class LevelManager
    {
        public static void AddLevel(string name, Level level) => _levels.Add(name, level);
        public static void SetCurrent(string name)
        {
            _currentLevel = _levels[name];
            _currentLevel.LoadContent();
        }

        public static void Update() => _currentLevel.Update();
        public static void Draw() => _currentLevel.Draw();

        private static Dictionary<string, Level> _levels = new();
        private static Level _currentLevel;
    }
}
