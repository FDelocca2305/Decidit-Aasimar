using Game.Core;
using Game.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.Interfaces;

namespace Game.Scripts.Enemies
{
    public class Boss : Enemy
    {
        public Boss() : base() {}

        public override void Initialize(float difficultyMultiplier)
        {
            currentHealth = 500;
            speed = 60;
            Transform.Scale = new Vector2(2.0f, 2.0f);
            Size = new Vector2(128, 128);
            InitializeAnimations();
        }
        protected override void InitializeAnimations()
        {
            animationManager.AddAnimation("run", AnimationFactory.CreateBossWalkAnimation());
            animationManager.AddAnimation("runBack", AnimationFactory.CreateBossWalkBackAnimation());
            animationManager.AddAnimation("bossDeath", AnimationFactory.CreateBossDeathAnimation());
            animationManager.AddAnimation("bossDeathBack", AnimationFactory.CreateBossDeathBackAnimation());
            animationManager.SetAnimation("run");
        }
    }
}
