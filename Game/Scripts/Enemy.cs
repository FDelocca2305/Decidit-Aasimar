using Game.Core;

namespace Game.Scripts
{
    public class Enemy : PhysicsObject
    {
        private Character player;
        private float speed;

        public Enemy(float x, float y, Character player, float speed) : base(new Vector2(x, y))
        {
            this.player = player;
            this.speed = speed;
        }

        public void Update(float deltaTime, TileMap map)
        {
            Vector2 direction = (player.Position - this.Position).Normalized;
            this.Velocity = direction * speed;

          
            base.Update(deltaTime, map);
        }


        public void Draw()
        {
            Engine.Draw("enemy.png", Position.x, Position.y);
        }
    }
}
