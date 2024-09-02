using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Enemy : GameObject
    {
        private Player player;
        private float speed = 20f;
        private float baseSpeed = 20f;
        private float baseHealth = 10;
        private float currentHealth;
        

        public Enemy(float x, float y, Player player, float difficultyMultiplier)
        {
            Position = new Vector2(x, y);
            this.player = player;
            IsActive = true;

            this.currentHealth = (int)(baseHealth * difficultyMultiplier);
            this.speed = baseSpeed * difficultyMultiplier;
        }

        public override void Update(float deltaTime)
        {
            MoveTowardsPlayer(deltaTime);
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
            Engine.Draw("Assets/enemy.png", Position.X, Position.Y);
        }
    }
}
