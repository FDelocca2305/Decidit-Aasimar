using Game.Core;
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

        private int experienceToNextLevel = 100;

        private float invulnerabilityTime = 0.5f;
        private float invulnerabilityTimer = 0f;

        public int Health { get; private set; } = 100;

        public float Attack => attack;
        
        public bool IsAttacking { get; set; }

        public int Experience { get; private set; } = 0;
        public int Level { get; private set; } = 1;

        public List<string> Upgrades { get; private set; } = new List<string>();

        public event Action<int> OnDamageTaken;
        public event Action<int> OnExperienceChanged;
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
                OnPositionChanged?.Invoke(Position); // Desencadenar el evento de cambio de posición
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

                OnDamageTaken?.Invoke(damage);

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
            OnExperienceChanged?.Invoke(Experience);
            orb.IsActive = false;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (Experience >= experienceToNextLevel)
            {
                Experience -= experienceToNextLevel;
                Level++;
                Console.WriteLine($"subio a nivel {Level}");
                experienceToNextLevel = CalculateExperienceForNextLevel();

                SelectUpgrade();
            }
        }

        private int CalculateExperienceForNextLevel()
        {
            return (int)(experienceToNextLevel * 1.5f);
        }

        private void SelectUpgrade()
        {
            List<string> possibleUpgrades = new List<string>
            {
                "Aumentar Velocidad",
                "Aumentar Daño",
                "Regeneración de Salud",
                "Disparo Rápido",
                "Aumentar Salud Máxima"
            };

            Random rand = new Random();
            string selectedUpgrade = possibleUpgrades[rand.Next(possibleUpgrades.Count)];
            Upgrades.Add(selectedUpgrade);
            Console.WriteLine($"Mejora desbloqueada: {selectedUpgrade}");

            ApplyUpgrade(selectedUpgrade);
        }

        private void ApplyUpgrade(string upgrade)
        {
            switch (upgrade)
            {
                case "Aumentar Velocidad":
                    speed += 20f;
                    break;
                case "Aumentar Daño":
                    attack *= 1.20f;
                    break;
                case "Regeneración de Salud":
                    Health += 20;
                    break;
                case "Disparo Rápido":
                    attackCooldown *= 0.95f;
                    break;
                case "Aumentar Salud Máxima":
                    Health += 50;
                    break;
                default:
                    break;
            }
        }
    }
}
