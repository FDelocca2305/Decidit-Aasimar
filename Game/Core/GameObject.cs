using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public bool IsActive { get; set; } = true;

        public abstract void Update(float deltaTime);
        public abstract void Render();

        public virtual bool CheckCollision(int width, int height, GameObject other)
        {
            return Position.X < other.Position.X + width &&
                   Position.X + width > other.Position.X &&
                   Position.Y < other.Position.Y + height &&
                   Position.Y + height > other.Position.Y;
        }
    }
}
