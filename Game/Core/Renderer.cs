using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Renderer
    {
        private Texture texture;
        private Vector2 baseSize;

        public Renderer(Texture texture, Vector2 baseSize)
        {
            this.texture = texture;
            this.baseSize = baseSize;
        }

        public void Draw(Transform transform)
        {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y);
        }
    }
}
