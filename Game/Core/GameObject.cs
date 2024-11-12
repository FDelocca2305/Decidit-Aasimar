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
        public Transform Transform { get; private set; } = new Transform();
        public Vector2 Size { get; protected set; }
        public bool IsActive { get; set; } = true;

        public abstract void Update(float deltaTime);
        public abstract void Render();

    }
}
