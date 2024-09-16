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

    }
}
