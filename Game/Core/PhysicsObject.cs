using System;

namespace Game.Core
    {
    public abstract class PhysicsObject : GameObject
    {
        protected Vector2 velocity;
        protected Vector2 acceleration;
        protected bool isGrounded;

        protected const float Gravity = 18.8f;
        protected const float TerminalVelocity = 50f;

        public PhysicsObject(Vector2 initialPosition) : base(initialPosition)
        {
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, Gravity);
            isGrounded = false;
        }

        public virtual void Update(float deltaTime, TileMap map)
        {
            ApplyPhysics(deltaTime, map);
        }

        public void ApplyPhysics(float deltaTime, TileMap map)
        {
            if (!isGrounded)
            {
                velocity += (acceleration * deltaTime * Gravity) * 2;
                if (velocity.y > TerminalVelocity)
                {
                    velocity = new Vector2(velocity.x, TerminalVelocity);
                }
            }

            Position += velocity * deltaTime;

            CheckCollision(map);
        }

        protected void CheckCollision(TileMap map)
        {
            if (map.CheckCollision(this))
            {
                isGrounded = true;
                velocity = new Vector2(velocity.x, 0);
            }
            else
            {
                isGrounded = false;
            }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
    }
}

