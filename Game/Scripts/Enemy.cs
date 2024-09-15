using Game.Core;
using System;
using System.Collections.Generic;

namespace Game.Scripts
{
    public class Enemy : GameObject
    {
        private Player player;
        private float speed = 20f;
        private float baseSpeed = 20f;
        private float baseHealth = 10;
        private float currentHealth;

        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;


        public Enemy(float x, float y, Player player, float difficultyMultiplier)
        {
            Position = new Vector2(x, y);
            this.player = player;
            IsActive = true;

            this.currentHealth = (int)(baseHealth * difficultyMultiplier);
            this.speed = baseSpeed * difficultyMultiplier;

            InitializeAnimations();
            currentAnimation = animations["run"];
        }

        private void InitializeAnimations()
        {
            InitializeRunAnimation();
            InitializeRunBackAnimation();
        }

        private void InitializeRunAnimation()
        {
            Animation run;
            var runningTextures = new List<Texture>();

            for (int i = 0; i <= 7; i++)
            {
                runningTextures.Add(Engine.GetTexture("Assets/Textures/Enemy/Walk/" + i + ".png"));
            }

            run = new Animation("run", 0.1f, runningTextures, true);
            animations.Add("run", run);
        }

        private void InitializeRunBackAnimation()
        {
            Animation runBack;
            var runningBackTextures = new List<Texture>();

            for (int i = 0; i <= 7; i++)
            {
                runningBackTextures.Add(Engine.GetTexture("Assets/Textures/Enemy/WalkBack/" + i + ".png"));
            }

            runBack = new Animation("run", 0.1f, runningBackTextures, true);
            animations.Add("runBack", runBack);
        }

        public override void Update(float deltaTime)
        {
            MoveTowardsPlayer(deltaTime);
            
            currentAnimation = Position.X <= player.Position.X ? animations["run"] : animations["runBack"];

            currentAnimation.Update(deltaTime);
        }

        private void MoveTowardsPlayer(float deltaTime)
        {
            float dirX = player.Position.X - Position.X;
            float dirY = player.Position.Y - Position.Y;
            float magnitude = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            if (magnitude > 0)
            {
                
                float newX = Position.X + (dirX / magnitude) * speed * deltaTime;
                float newY = Position.Y + (dirY / magnitude) * speed * deltaTime;

                Position = new Vector2(newX, newY);
            }
        }

        public void TakeDamage(float damage)
        {
            this.currentHealth -= damage;

            if (currentHealth <= 0)
            {
                IsActive = false;
            }
        }

        public override void Render()
        {
            Engine.Draw(currentAnimation.CurrentTexture, Position.X, Position.Y);
        }
    }
}
