﻿using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Player : GameObject
    {
        private float speed = 100f;

        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;
        

        private float attackCooldown = 2.0f;
        private float attackDuration = 1f;
        private float attackTimer = 0f;
        private float attackDurationTimer = 0f;

        private float attack = 10f;

        private float invulnerabilityTime = 0.5f;
        private float invulnerabilityTimer = 0f;

        public int Health { get; private set; } = 100;
        public float MaxHealth { get; private set; } = 100;

        public float Attack => attack;
        
        public bool IsAttacking { get; private set; }

        public int Experience { get; private set; } = 0;
        public int ExperienceToNextLevel { get; private set; } = 100;
        public int Level { get; private set; } = 1;

        public event Action<int, float> OnDamageTaken;
        public event Action<int, int> OnExperienceChanged;
        public event Action<Vector2> OnPositionChanged;

        public Player(float x, float y)
        {
            Position = new Vector2(x, y);

            InitializeAnimations();
            currentAnimation = animations["idle"];
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
                currentAnimation = animations["run"];
            }
            else if (Engine.GetKey(Keys.LEFT) || Engine.GetKey(Keys.UP) || Engine.GetKey(Keys.DOWN))
            {
                currentAnimation = animations["runBack"];
            }
            else
            {
                currentAnimation = animations["idle"];
            }

            if (IsAttacking)
            {
                currentAnimation = animations["attack"];
            }

            currentAnimation.Update(deltaTime);
        }

        private void HandleInput(float deltaTime)
        {
            var position = Position;
            Vector2 previousPosition = Position;

            if (Engine.GetKey(Keys.UP))
                position = new Vector2(position.X, position.Y - speed * deltaTime);
            if (Engine.GetKey(Keys.DOWN))
                position = new Vector2(position.X, position.Y + speed * deltaTime);
            if (Engine.GetKey(Keys.LEFT))
                position = new Vector2(position.X - speed * deltaTime, position.Y);
            if (Engine.GetKey(Keys.RIGHT))
                position = new Vector2(position.X + speed * deltaTime, position.Y);

            Position = position;

            if (previousPosition != Position)
            {
                OnPositionChanged?.Invoke(Position);
            }
        }

        private void InitializeIdleAnimation()
        {
            Animation idle;
            var idleTextures = new List<Texture>();
            idleTextures.Add(Engine.GetTexture("Assets/Textures/Player/player.png"));
            idle = new Animation("idle", 1f, idleTextures, true);
            animations.Add("idle", idle);
        }

        private void InitializeRunAnimation()
        {
            Animation run;
            var runningTextures = new List<Texture>();

            for (int i = 0; i <= 7; i++)
            {
                runningTextures.Add(Engine.GetTexture("Assets/Textures/Player/Run/" + i + ".png"));
            }

            run = new Animation("run", 0.1f, runningTextures, true);
            animations.Add("run", run);
        }

        private void InitializeRunBackAnimation()
        {
            Animation runBack;
            var runningBackTextures = new List<Texture>();

            for (int i = 0; i <= 6; i++)
            {
                runningBackTextures.Add(Engine.GetTexture("Assets/Textures/Player/RunBack/" + i + ".png"));
            }

            runBack = new Animation("run", 0.1f, runningBackTextures, true);
            animations.Add("runBack", runBack);
        }

        private void InitializeAttackAnimation()
        {
            Animation attack;
            
            var attackTextures = new List<Texture>();

            for (int i = 0; i <= 10; i++)
            {
                attackTextures.Add(Engine.GetTexture("Assets/Textures/Player/Attack/" + i + ".png"));
            }

            attack = new Animation("attack", .25f, attackTextures, true);

            animations.Add("attack", attack);
        }

        private void InitializeAnimations()
        {
            InitializeIdleAnimation();
            InitializeAttackAnimation();
            InitializeRunAnimation();
            InitializeRunBackAnimation();
        }

        public void TakeDamage(int damage)
        {
            if (invulnerabilityTimer <= 0)
            {
                Health -= damage;
                invulnerabilityTimer = invulnerabilityTime;

                OnDamageTaken?.Invoke(damage, MaxHealth);

                if (Health <= 0)
                {
                    Health = 0;
                    IsActive = false;
                }
            }
        }

        public override void Render()
        {
            Engine.Draw(currentAnimation.CurrentTexture, Position.X, Position.Y);
        }

        public void CollectExperience(ExperienceOrb orb)
        {
            Experience += orb.ExperiencePoints;
            OnExperienceChanged?.Invoke(Experience, ExperienceToNextLevel);
            orb.IsActive = false;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (Experience >= ExperienceToNextLevel)
            {
                Experience -= ExperienceToNextLevel;
                Level++;
                Console.WriteLine($"Subió a nivel {Level}");
                ExperienceToNextLevel = CalculateExperienceForNextLevel();

                GameManager.Instance.UpgradeManager.ShowUpgradeOptions();
            }
        }

        private int CalculateExperienceForNextLevel()
        {
            return (int)(ExperienceToNextLevel * 1.5f);
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
            OnDamageTaken?.Invoke(Health, MaxHealth); 
        }
    }
}
