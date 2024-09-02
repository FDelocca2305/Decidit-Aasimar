using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Scripts
{
    public class ExperienceOrb : GameObject
    {
        public int ExperiencePoints { get; private set; }

        public ExperienceOrb(float x, float y, int experiencePoints)
        {
            Position = new Vector2(x, y);
            ExperiencePoints = experiencePoints;
            IsActive = true;
        }

        public override void Update(float deltaTime)
        {
            
        }

        public override void Render()
        {
            Engine.Draw("Assets/experience_orb.png", Position.X, Position.Y, 0.2f, 0.2f);
        }

        public bool CheckCollision(Player player)
        {
            float distance = (float)Math.Sqrt(
                Math.Pow(player.Position.X - Position.X, 2) +
                Math.Pow(player.Position.Y - Position.Y, 2)
            );

            return distance < 20f;
        }
    }
}
