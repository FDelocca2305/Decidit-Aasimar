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

        public Renderer(Texture texture)
        {
            this.texture = texture;
        }

        public void SetTexture(Texture texture)
        {
            this.texture = texture;
        }

        public void Draw(Transform transform)
        {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y);
        }
    }
}
