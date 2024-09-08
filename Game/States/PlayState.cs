﻿using Game.Core;
using Game.Scripts;
using System;

namespace Game.States
{
    public class PlayState : IGameState
    {
        private Player player;
        private ObjectManager objectManager;
        private GameTimer gameTimer;
        private WaveManager waveManager;
        private UIManager uiManager;

        public void Enter()
        {
            Console.WriteLine("Comenzando el Juego");
            player = new Player(960f, 540f);
            objectManager = new ObjectManager(player);
            objectManager.Add(player);
            gameTimer = new GameTimer();
            waveManager = new WaveManager(objectManager, player);
            uiManager = new UIManager(player);
        }

        public void Update(float deltaTime)
        {
            Engine.Clear(135, 206, 235);
            Engine.Draw("Assets/floor.png");
            
            gameTimer.Update(deltaTime);
            objectManager.UpdateAll(deltaTime, player);
            
            waveManager.Update(deltaTime);
            
            Engine.Show();

            if (player.Health <= 0)
            {
                GameManager.Instance.ChangeState(new GameOverState());
            }

            if (gameTimer.TimeElapsedInMinutes == 5f)
            {
                GameManager.Instance.ChangeState(new VictoryState());
            }

            if (Engine.GetKey(Keys.ESCAPE))
                GameManager.Instance.ChangeState(new MainMenuState());
        }

        public void Render()
        {
            objectManager.RenderAll();
            uiManager.Render(gameTimer.GetFormattedTime());
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Juego");
        }

        
    }
}
