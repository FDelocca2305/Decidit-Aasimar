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
        public FastEnemy() : base() { }

        public override void Initialize(float difficultyMultiplier)
        {
            speed = 40f * difficultyMultiplier;
            currentHealth = 5 * difficultyMultiplier;

            InitializeAnimations();
        }

        protected override void InitializeAnimations()
        {
            base.InitializeAnimations();
        }
    }
}
