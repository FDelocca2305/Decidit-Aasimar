using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class AnimationManager
    {
        private readonly Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;

        public void AddAnimation(string name, Animation animation)
        {
            animations[name] = animation;
        }

        public void SetAnimation(string name)
        {
            if (animations.ContainsKey(name))
                currentAnimation = animations[name];
        }

        public void Update(float deltaTime)
        {
            currentAnimation?.Update(deltaTime);
        }

        public Texture GetCurrentTexture()
        {
            return currentAnimation?.CurrentTexture;
        }
    }
}
