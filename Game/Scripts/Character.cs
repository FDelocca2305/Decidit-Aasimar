using Game.Core;

namespace Game.Scripts
{
    public class Character : GameObject
    {
        private float _speed;
        private float _timeMoving;
        private float _maxTime;

        public Character(Vector2 initialPosition, float speed, float maxTimeInSeconds)
            : base(initialPosition)
        {
            _speed = speed;
            _maxTime = maxTimeInSeconds;
            _timeMoving = 0f;
        }

        public void Update(float deltaTime)
        {
            if (_timeMoving < _maxTime)
            {
                Position += new Vector2(_speed * deltaTime, 0f);
                _timeMoving += deltaTime;
            }
        }
    }
}