using Game.Core;
using Game.Core.Interfaces;
using Game.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.Utils;

namespace Game.Scripts
{
    public class Player : GameObject, IRenderizable
    {
        private float speed =  ConfigLoader.PlayerConfig.Speed;

        private AnimationManager animationManager = new AnimationManager();
        private IUIManager uiManager;
        private ILevelManager levelManager;

        private float attackCooldown = ConfigLoader.PlayerConfig.AttackCooldown;
        private float attackDuration = ConfigLoader.PlayerConfig.AttackDuration;
        private float attackTimer = 0f;
        private float attackDurationTimer = 0f;
        private float attack = ConfigLoader.PlayerConfig.Attack;

        private float invulnerabilityTime = ConfigLoader.PlayerConfig.InvulnerabilityTime;
        private float invulnerabilityTimer = 0f;

        public int Health { get; private set; } = ConfigLoader.PlayerConfig.Health;
        public float MaxHealth { get; private set; } = ConfigLoader.PlayerConfig.MaxHealth;

        public float Attack => attack;
        
        public bool IsAttacking { get; private set; }
        public Vector2 AttackColliderSize { get; private set; }
        public Renderer Renderer { get; }

        public Player(float x, float y, IUIManager uiManager, ILevelManager levelManager)
        {
            Transform.Position = new Vector2(x, y);
            this.Size = new Vector2(30, 44);
            AttackColliderSize = new Vector2(100, 50);

            this.uiManager = uiManager;
            this.levelManager = levelManager;
            levelManager.OnLevelUp += HandleLevelUp;

            InitializeAnimations();
            animationManager.SetAnimation("idle");

            uiManager.UpdateHealth(Health, MaxHealth);
            uiManager.UpdatePlayerPosition(Transform.Position);

            Renderer = new Renderer(animationManager.GetCurrentTexture(), Size);
        }

        public override void Update(float deltaTime)
        {
            HandleInput(deltaTime);
            HandleAnimation(deltaTime);
            HandleAttack(deltaTime);
            HandleInvulnerability(deltaTime);
        }

        private void HandleInvulnerability(float deltaTime)
        {
            if (invulnerabilityTimer > 0)
            {
                invulnerabilityTimer -= deltaTime;
            }
        }

        private void HandleAttack(float deltaTime)
        {
            attackTimer += deltaTime;

            if (attackTimer >= attackCooldown)
            {
                IsAttacking = true;
                attackTimer = 0f;
                attackDurationTimer = 0f;
            }

            if (IsAttacking)
            {
                attackDurationTimer += deltaTime;
                if (attackDurationTimer >= attackDuration)
                {
                    IsAttacking = false;
                }
            }
        }

        private void HandleAnimation(float deltaTime)
        {
            if (Engine.GetKey(Keys.RIGHT) || Engine.GetKey(Keys.UP) || Engine.GetKey(Keys.DOWN))
            {
                animationManager.SetAnimation("run");
            }
            else if (Engine.GetKey(Keys.LEFT) || Engine.GetKey(Keys.UP) || Engine.GetKey(Keys.DOWN))
            {
                animationManager.SetAnimation("runBack");
            }
            else
            {
                animationManager.SetAnimation("idle");
            }

            if (IsAttacking)
            {
                animationManager.SetAnimation("attack");
            }

            animationManager.Update(deltaTime);
        }

        private void HandleInput(float deltaTime)
        {
            var position = Transform.Position;
            Vector2 previousPosition = Transform.Position;

            if (InputManager.IsMoveUp())
                position = new Vector2(position.X, position.Y - speed * deltaTime);
            if (InputManager.IsMoveDown())
                position = new Vector2(position.X, position.Y + speed * deltaTime);
            if (InputManager.IsMoveLeft())
                position = new Vector2(position.X - speed * deltaTime, position.Y);
            if (InputManager.IsMoveRight())
                position = new Vector2(position.X + speed * deltaTime, position.Y);

            Transform.Position = position;

            if (previousPosition != Transform.Position)
            {
                uiManager.UpdatePlayerPosition(Transform.Position);
            }
        }

        private void InitializeAnimations()
        {
            animationManager.AddAnimation("runBack", AnimationFactory.CreateRunBackAnimation());
            animationManager.AddAnimation("idle", AnimationFactory.CreateIdleAnimation());
            animationManager.AddAnimation("run", AnimationFactory.CreateRunAnimation());
            animationManager.AddAnimation("attack", AnimationFactory.CreateAttackAnimation());
        }

        public void TakeDamage(int damage)
        {
            if (invulnerabilityTimer <= 0)
            {
                Health -= damage;
                invulnerabilityTimer = invulnerabilityTime;

                uiManager.UpdateHealth(Health, MaxHealth);
                if (Health <= 0)
                {
                    Health = 0;
                    IsActive = false;
                }
            }
        }

        public override void Render()
        {
            Renderer.SetTexture(animationManager.GetCurrentTexture());
            Renderer.Draw(Transform);
        }

        public void CollectExperience(ExperienceOrb orb)
        {
            levelManager.AddExperience(orb.ExperiencePoints);
            uiManager.UpdateExperience(levelManager.Experience, levelManager.ExperienceToNextLevel);
        }

        private void HandleLevelUp()
        {
            GameManager.Instance.UpgradeManager.ShowUpgradeOptions();
        }

        public void IncreaseSpeed(float amount)
        {
            speed += amount;
        }

        public void IncreaseDamage(float multiplier)
        {
            attack *= multiplier;
        }

        public void RegenerateHealth(int amount)
        {
            Health += amount;
        }

        public void ReduceAttackCooldown(float multiplier)
        {
            attackCooldown *= multiplier;
        }

        public void IncreaseMaxHealth(int amount)
        {
            MaxHealth += amount;
            Health = Math.Min(Health + amount, (int)MaxHealth);
            uiManager.UpdateHealth(Health, MaxHealth);
        }
    }
}
