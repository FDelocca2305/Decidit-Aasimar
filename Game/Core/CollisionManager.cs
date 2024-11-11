using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class CollisionManager
    {
        public static bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            float distanceX = Math.Abs(positionA.X - positionB.X);
            float distanceY = Math.Abs(positionA.Y - positionB.Y);

            float sumHalfWidths = sizeA.X / 2 + sizeB.X / 2;
            float sumHalfHeights = sizeA.Y / 2 + sizeB.Y / 2;

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeights;
        }

        public void CheckCollisions(Player player, IEnumerable<GameObject> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if(obj is Enemy enemy && enemy.IsActive)
                {
                    if (IsBoxColliding(player.Position, player.Size, enemy.Position, enemy.Size))
                    {
                        player.TakeDamage(10);
                    }

                    if (player.IsAttacking && IsBoxColliding(player.Position, player.AttackColliderSize, enemy.Position, enemy.Size))
                    {
                        enemy.TakeDamage(player.Attack);
                    }
                }

                if (obj is ExperienceOrb orb && orb.IsActive && orb.CheckCollision(player))
                {
                    player.CollectExperience(orb);
                    orb.IsActive = false;
                }
            }
        }
    }
}
