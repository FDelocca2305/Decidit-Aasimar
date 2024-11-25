using Game.Core;
using Game.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game.Scripts
{
    public abstract class Enemy : GameObject, IRenderizable
    {
        protected Player player;
        protected float speed;
        protected float currentHealth;

        protected AnimationManager animationManager = new AnimationManager();

        public event Action<Enemy> OnDeath;
        public Renderer Renderer { get; }

        public float CurrentHealth { get { return currentHealth; } }
        public float Speed { get { return speed; } }
        protected Enemy()
        {
            IsActive = true;
            Size = new Vector2(48, 60);
            InitializeAnimations();
            Renderer = new Renderer(animationManager.GetCurrentTexture());
        }
        public void Reset(Vector2 position, float health, float speed, Player player)
        {
            Transform.Position = position;
            currentHealth = health;
            this.speed = speed;
            this.player = player;
            IsActive = true;
            this.Size = new Vector2(48, 60);
            animationManager.SetAnimation("run");
            Console.WriteLine($"Resetting Enemy: Speed={speed}, Health={health}");
        }

        public abstract void Initialize(float difficultyMultiplier);

        protected virtual void InitializeAnimations()
        {
            animationManager.AddAnimation("run", AnimationFactory.CreateBasicEnemyRunAnimation());
            animationManager.AddAnimation("runBack", AnimationFactory.CreateBasicEnemyRunBackAnimation());
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
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void MoveTowardsPlayer(float deltaTime)
        {
            if (speed <= 0)
            {
                return;
            }

            float clampedDeltaTime = Clamp(deltaTime, 0.001f, 0.1f);

            float dirX = player.Transform.Position.X - Transform.Position.X;
            float dirY = player.Transform.Position.Y - Transform.Position.Y;
            float magnitude = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            if (magnitude == 0)
            {
                return;
            }

            if (magnitude > 0)
            {
                
                float newX = Transform.Position.X + (dirX / magnitude) * speed * clampedDeltaTime;
                float newY = Transform.Position.Y + (dirY / magnitude) * speed * clampedDeltaTime;

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

        public void Render()
        {
            var texture = animationManager.GetCurrentTexture();
            if (texture != null)
            {
                Renderer.SetTexture(animationManager.GetCurrentTexture());
                Renderer.Draw(Transform);
            }
            else
            {
                Console.WriteLine("Warning: Current texture is null for Enemy rendering.");
            }
        }
    }
}
