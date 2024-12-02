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
        private float invulnerabilityTime = 1.0f;
        private float invulnerabilityTimer = 0f;
        private float maxHealth = ConfigLoader.EnemyConfig.Boss.Health;

        public float MaxHealth => maxHealth;

        public Boss() : base() {}

        public override void Initialize(float difficultyMultiplier)
        {
            currentHealth = maxHealth;
            speed = ConfigLoader.EnemyConfig.Boss.Speed;
            Transform.Scale = new Vector2(2.0f, 2.0f);
            Size = new Vector2(80, 80);
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
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (invulnerabilityTimer > 0)
            {
                invulnerabilityTimer -= deltaTime;
            }
        }

        public override void TakeDamage(float damage)
        {
            if (invulnerabilityTimer > 0)
            {
                return;
            }
            
            base.TakeDamage(damage);
            invulnerabilityTimer = invulnerabilityTime;
        }
    }
}
