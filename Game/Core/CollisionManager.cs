using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.Utils;

namespace Game.Core
{
    public class CollisionManager
    {
        private Quadtree quadtree;

        public CollisionManager()
        {
            quadtree = new Quadtree(0, new Rectangle(0, 0,GlobalConstants.GraphicsConstants.ScreenWidth, GlobalConstants.GraphicsConstants.ScreenHeight));
        }

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
            quadtree.Clear();
            foreach (var obj in gameObjects)
            {
                quadtree.Insert(obj);
            }

            List<GameObject> possibleCollisions = new List<GameObject>();

            foreach (var obj in gameObjects)
            {
                if (obj.IsActive)
                {
                    possibleCollisions.Clear();
                    quadtree.Retrieve(possibleCollisions, obj);

                    foreach (var otherObj in possibleCollisions)
                    {
                        if (obj is Enemy enemy && enemy.IsActive)
                        {
                            if (IsBoxColliding(player.Transform.Position, player.Size, enemy.Transform.Position, enemy.Size))
                            {
                                player.TakeDamage(ConfigLoader.PlayerConfig.BaseDamageTaken);
                            }

                            if (player.IsAttacking && IsBoxColliding(player.Transform.Position, player.AttackColliderSize, enemy.Transform.Position, enemy.Size))
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
    }
}
