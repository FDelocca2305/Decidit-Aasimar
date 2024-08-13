using System;

namespace Game.Core
{
    public class Vector2
    {
        public float x {
            get;
        }
        
        public float y {
            get;
        }

        public Vector2(float x = 0f, float y = 0f)
        {
            this.x = x;
            this.y = y;
        }
        
        public float Magnitude => (float)Math.Sqrt(x * x + y * y);
        
        public Vector2 Normalized => new Vector2(x / Magnitude, y / Magnitude);
        
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        
        public static Vector2 operator *(Vector2 a, float scalar)
        {
            return new Vector2(a.x * scalar, a.y * scalar);
        }
        
        public static Vector2 operator /(Vector2 a, float scalar)
        {
            return new Vector2(a.x / scalar, a.y / scalar);
        }
        
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }
        
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Magnitude;
        }
        
        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}