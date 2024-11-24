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
            speed = ConfigLoader.EnemyConfig.FastEnemy.Speed * difficultyMultiplier;
            currentHealth = ConfigLoader.EnemyConfig.FastEnemy.Health * difficultyMultiplier;

            InitializeAnimations();
        }
        
        protected override void InitializeAnimations()
        {
            base.InitializeAnimations();
        }
    }
}
