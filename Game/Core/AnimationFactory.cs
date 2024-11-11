using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public static class AnimationFactory
    {
        public static Animation CreateEnemyRunAnimation()
        {
            var runningTextures = new List<Texture>();
            for (int i = 0; i <= 7; i++)
            {
                runningTextures.Add(Engine.GetTexture($"Assets/Textures/Enemy/Walk/{i}.png"));
            }
            return new Animation("run", 0.1f, runningTextures, true);
        }

        public static Animation CreateEnemyRunBackAnimation()
        {
            var runningBackTextures = new List<Texture>();
            for (int i = 0; i <= 7; i++)
            {
                runningBackTextures.Add(Engine.GetTexture($"Assets/Textures/Enemy/WalkBack/{i}.png"));
            }
            return new Animation("runBack", 0.1f, runningBackTextures, true);
        }
    }
}
