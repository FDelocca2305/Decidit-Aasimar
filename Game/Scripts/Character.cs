using Game.Core;

namespace Game.Scripts
{
    public class Character : PhysicsObject
    {
        private const float Speed = 150f;
        private const float JumpForce = -350f;
        private float screenWidth;
        private float screenHeight;
        private float scale;

        public Character(Vector2 initialPosition, float screenWidth, float screenHeight, float scale = 1.0f) : base(initialPosition)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.scale = scale;
        }

        public void Update(float deltaTime, TileMap map)
        {
            HandleInput();
            ApplyPhysics(deltaTime, map);
            ClampPosition();
        }

        private void HandleInput()
        {
            if (Engine.GetKey(Keys.LEFT))
                MoveLeft();
            else if (Engine.GetKey(Keys.RIGHT))
                MoveRight();
            else
                StopHorizontalMovement();

            if (Engine.GetKey(Keys.UP) && isGrounded)
                Jump();
        }

        private void MoveLeft()
        {
            velocity = new Vector2(-Speed, velocity.y);
        }

        private void MoveRight()
        {
            velocity = new Vector2(Speed, velocity.y);
        }

        private void StopHorizontalMovement()
        {
            velocity = new Vector2(0, velocity.y);
        }

        private void Jump()
        {
            velocity = new Vector2(velocity.x, JumpForce);
            isGrounded = false;
        }

        private void ClampPosition()
        {
            if (Position.x < 0)
                Position = new Vector2(0, Position.y);
            else if (Position.x > screenWidth - GetCharacterWidth())
                Position = new Vector2(screenWidth - GetCharacterWidth(), Position.y);
        }

        private float GetCharacterWidth()
        {
            return Engine.GetTexture("character.png").Width * scale;
        }
    }
}