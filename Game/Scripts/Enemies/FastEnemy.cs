using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Enemies
{
    public class FastEnemy : Enemy
    {
        public FastEnemy(float x, float y, Player player) : base(x, y, player) { }

        public override void Initialize(float difficultyMultiplier)
        {
            speed = 40f * difficultyMultiplier;
            currentHealth = 5 * difficultyMultiplier;

            InitializeAnimations();
        }

        protected override void InitializeAnimations()
        {
            Animation run;
            var runningTextures = new List<Texture>();

            for (int i = 0; i <= 7; i++)
            {
                runningTextures.Add(Engine.GetTexture($"Assets/Textures/Enemy/WalkBack/{i}.png"));
            }

            run = new Animation("run", 0.1f, runningTextures, true);
            animations.Add("run", run);

            Animation runBack;
            var runningBackTextures = new List<Texture>();

            for (int i = 0; i <= 7; i++)
            {
                runningBackTextures.Add(Engine.GetTexture($"Assets/Textures/Enemy/WalkBack/{i}.png"));
            }

            runBack = new Animation("runBack", 0.1f, runningBackTextures, true);
            animations.Add("runBack", runBack);

            currentAnimation = animations["run"];
        }
    }
}
