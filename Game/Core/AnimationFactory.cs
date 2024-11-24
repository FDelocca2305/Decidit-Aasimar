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

        public static Animation CreateIdleAnimation() =>
        new Animation("idle", 1f, new List<Texture> { Engine.GetTexture("Assets/Textures/Player/player.png") }, true);

        public static Animation CreateRunAnimation()
        {
            var runningTextures = new List<Texture>();
            for (int i = 0; i <= 7; i++)
            {
                runningTextures.Add(Engine.GetTexture("Assets/Textures/Player/Run/" + i + ".png"));
            }
            return new Animation("run", 0.1f, runningTextures, true);
        }

        public static Animation CreateRunBackAnimation()
        {
            var runningTextures = new List<Texture>();
            for (int i = 0; i <= 6; i++)
            {
                runningTextures.Add(Engine.GetTexture("Assets/Textures/Player/RunBack/" + i + ".png"));
            }
            return new Animation("run", 0.1f, runningTextures, true);
        }

        public static Animation CreateAttackAnimation()
        {
            var attackTextures = new List<Texture>();
            for (int i = 0; i <= 10; i++)
            {
                attackTextures.Add(Engine.GetTexture("Assets/Textures/Player/Attack/" + i + ".png"));
            }
            return new Animation("attack", 0.25f, attackTextures, true);
        }

        public static Animation CreateExperienceOrbAnimation() =>
        new Animation("experienceOrb", 1f, new List<Texture> { Engine.GetTexture("Assets/experience_orb.png") }, true);
    }
}
