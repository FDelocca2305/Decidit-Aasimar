using Game.Core;
using System;
using System.Collections.Generic;

namespace Game.Scripts
{
    public abstract class Enemy : GameObject
    {
        protected Player player;
        protected float speed;
        protected float baseSpeed = 20f;
        protected float baseHealth = 10;
        protected float currentHealth;

        protected AnimationManager animationManager = new AnimationManager();

        //public Vector2 Size { get; private set; }

        public event Action<Enemy> OnDeath;


        protected Enemy(float x, float y, Player player)
        {
            Transform.Position = new Vector2(x, y);
            this.player = player;
            this.Size = new Vector2(48, 60);
            IsActive = true;
        }

        public abstract void Initialize(float difficultyMultiplier);

        protected virtual void InitializeAnimations()
        {
            animationManager.AddAnimation("run", AnimationFactory.CreateEnemyRunAnimation());
            animationManager.AddAnimation("runBack", AnimationFactory.CreateEnemyRunBackAnimation());
            animationManager.SetAnimation("run");
        }

        public override void Update(float deltaTime)
        {
            MoveTowardsPlayer(deltaTime);

            if (Transform.Position.X <= player.Transform.Position.X)
                animationManager.SetAnimation("run");
            else
                animationManager.SetAnimation("runBack");

            animationManager.Update(deltaTime);
        }

        private void MoveTowardsPlayer(float deltaTime)
        {
            float dirX = player.Transform.Position.X - Transform.Position.X;
            float dirY = player.Transform.Position.Y - Transform.Position.Y;
            float magnitude = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            if (magnitude > 0)
            {
                
                float newX = Transform.Position.X + (dirX / magnitude) * speed * deltaTime;
                float newY = Transform.Position.Y + (dirY / magnitude) * speed * deltaTime;

                Transform.Position = new Vector2(newX, newY);
            }
        }

        public void TakeDamage(float damage)
        {
            this.currentHealth -= damage;

            if (currentHealth <= 0)
            {
                IsActive = false;
                OnDeath?.Invoke(this);
            }
        }

        public override void Render()
        {
            if (animationManager.GetCurrentTexture() != null)
            {
                Engine.Draw(animationManager.GetCurrentTexture(), Transform.Position.X, Transform.Position.Y);
            }
        }
    }
}
