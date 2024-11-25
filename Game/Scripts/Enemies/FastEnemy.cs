using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.Utils;

namespace Game.Scripts.Enemies
{
    public class FastEnemy : Enemy
    {
        public FastEnemy() : base() { }

        public override void Initialize(float difficultyMultiplier)
        {
            speed = ConfigLoader.EnemyConfig.FastEnemy.Speed;
            currentHealth = ConfigLoader.EnemyConfig.FastEnemy.Health * difficultyMultiplier;
            Transform.Scale = new Vector2 (1.5f, 1.5f);
            InitializeAnimations();
        }

        protected override void InitializeAnimations()
        {
            animationManager.AddAnimation("run", AnimationFactory.CreateFastEnemyRunAnimation());
            animationManager.AddAnimation("runBack", AnimationFactory.CreateFastEnemyRunBackAnimation());
            animationManager.SetAnimation("run");
        }
    }
}
