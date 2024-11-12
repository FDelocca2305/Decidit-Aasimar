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
            Transform.Position = new Vector2(x, y);
            ExperiencePoints = experiencePoints;
            IsActive = true;
            this.Size = new Vector2(25.6f, 25.6f);
        }

        public override void Update(float deltaTime)
        {
            
        }

        public override void Render()
        {
            Engine.Draw("Assets/experience_orb.png", Transform.Position.X, Transform.Position.Y, 0.2f, 0.2f);
        }

        public bool CheckCollision(Player player)
        {
            float distance = (float)Math.Sqrt(
                Math.Pow(player.Transform.Position.X - Transform.Position.X, 2) +
                Math.Pow(player.Transform.Position.Y - Transform.Position.Y, 2)
            );

            return distance < 20f;
        }
    }
}
