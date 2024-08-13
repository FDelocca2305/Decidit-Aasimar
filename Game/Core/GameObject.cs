namespace Game.Core
{
    public class GameObject
    {
        public Vector2 Position { get; set; }

        public GameObject(Vector2 initialPosition)
        {
            Position = initialPosition;
        }
    }
}