using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Enemies
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy() : base() { }

        public override void Initialize(float difficultyMultiplier)
        {
            currentHealth = baseHealth * difficultyMultiplier;
            speed = baseSpeed * difficultyMultiplier;

            InitializeAnimations();
        }

        protected override void InitializeAnimations()
        {
            base.InitializeAnimations();
        }
    }
}
