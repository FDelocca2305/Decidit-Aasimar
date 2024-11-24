using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.Utils;

namespace Game.Scripts.Enemies
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy() : base() { }

        public override void Initialize(float difficultyMultiplier)
        {
            currentHealth = ConfigLoader.EnemyConfig.BasicEnemy.Speed * difficultyMultiplier;
            speed = ConfigLoader.EnemyConfig.BasicEnemy.Health * difficultyMultiplier;

            InitializeAnimations();
        }
        
        protected override void InitializeAnimations()
        {
            base.InitializeAnimations();
        }
    }
}
