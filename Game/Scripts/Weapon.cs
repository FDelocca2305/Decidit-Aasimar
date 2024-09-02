using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{

    public class Weapon
    {
        private const int ENEMY_WIDTH = 74;
        private const int ENEMY_HEIGHT = 41;

        private Player player;
        private float range;
        private float cooldown;
        private float cooldownTimer = 0f;

        public bool CanAttack { get; private set; }

        public Weapon(Player player, float range, float cooldown)
        {
            this.player = player;
            this.range = range;
            this.cooldown = cooldown;
        }

        public void Update(float deltaTime)
        {
            cooldownTimer += deltaTime;
            if (cooldownTimer >= cooldown)
            {
                Attack();
                cooldownTimer = 0f;
            }
            else
            {
                CanNotAttackAnymore();
            }
        }

        private void Attack()
        {
            CanAttack = true;
        }

        private void CanNotAttackAnymore()
        {
            CanAttack = false;
        }

        public bool CheckCollision(Enemy enemy)
        {
            return player.CheckCollision(ENEMY_WIDTH, ENEMY_HEIGHT, enemy);
        }
    }

}
