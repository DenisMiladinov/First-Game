using Microsoft.Xna.Framework;

namespace FirstGame
{
    internal class Camera
    {
        public Camera() 
        {
            ProjectionViewMatrix = Matrix.Identity;
        }

        public void FollowTarget(float x, float y) 
        {
            ProjectionViewMatrix = Matrix.CreateTranslation(x, y, 0.0f);
        }

        public Matrix ProjectionViewMatrix { get; set; }
    }
}
