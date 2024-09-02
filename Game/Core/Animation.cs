using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Animation
    {
        private bool isLoop;
        private string name;
        private float speed;
        private float currentTime;
        private int currentFrame = 0;
        private List<Texture> textures = new List<Texture>();

        public Texture CurrentTexture => textures[currentFrame];

        public Animation(string name, float speed, List<Texture> textures = null, bool isLoop = true)
        {
            this.name = name;
            this.speed = speed;
            this.isLoop = isLoop;
            this.currentFrame = 0;

            if (textures != null)
            {
                this.textures = textures;
            }
        }

        public void Update(float deltaTime)
        {
            currentTime += deltaTime;

            if (currentTime >= speed)
            {
                currentTime = 0f;
                currentFrame++;

                if (currentFrame >= textures.Count)
                {
                    if (isLoop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = textures.Count - 1;
                    }
                }
            }
        }
    }
}
