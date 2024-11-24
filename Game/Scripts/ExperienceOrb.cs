using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;
using Game.Scripts.Interfaces;

namespace Game.Scripts
{
    public class ExperienceOrb : GameObject, IRenderizable
    {
        public int ExperiencePoints { get; private set; }
        public Renderer Renderer { get; }

        protected AnimationManager animationManager = new AnimationManager();

        public ExperienceOrb(float x, float y, int experiencePoints)
        {
            Transform.Position = new Vector2(x, y);
            Transform.Scale = new Vector2(0.2f, 0.2f);
            ExperiencePoints = experiencePoints;
            IsActive = true;
            this.Size = new Vector2(25.6f, 25.6f);
            InitializeAnimations();
            Renderer = new Renderer(animationManager.GetCurrentTexture());
        }

        public override void Update(float deltaTime)
        {
            
        }

        //public void Render()
        //{
        //    Engine.Draw("Assets/experience_orb.png", Transform.Position.X, Transform.Position.Y, 0.2f, 0.2f);
        //}
        public void Render()
        {
            Renderer.SetTexture(animationManager.GetCurrentTexture());
            Renderer.Draw(Transform);
        }

        protected virtual void InitializeAnimations()
        {
            animationManager.AddAnimation("experienceOrb", AnimationFactory.CreateExperienceOrbAnimation());
            animationManager.SetAnimation("experienceOrb");
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
