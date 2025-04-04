﻿using Game.Core;
using Game.Scripts;
using Game.Scripts.Waves;
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
        private LevelManager levelManager;
        public void Enter()
        {
            uiManager = new UIManager();
            levelManager = new LevelManager();
            Console.WriteLine("Comenzando el Juego");
            player = new Player(960f, 540f, uiManager, levelManager);
            objectManager = new ObjectManager(player);
            objectManager.Add(player);
            gameTimer = new GameTimer();
            waveManager = new WaveManager(objectManager, player);
            GameManager.Instance.InitializeUpgradeSystem(player, uiManager);
        }

        public void Update(float deltaTime)
        {
            gameTimer.Update(deltaTime);
            objectManager.UpdateAll(deltaTime, player);
            
            waveManager.Update(deltaTime);

            if (player.Health <= 0)
            {
                GameManager.Instance.ChangeState(new GameOverState());
            }

            if (gameTimer.TimeElapsedInMinutes == 2f)
            {
                GameManager.Instance.ChangeState(new VictoryState());
            }

            if (Engine.GetKey(Keys.ESCAPE))
                GameManager.Instance.ChangeState(new MainMenuState());

            uiManager.HandleUpgradeSelection();
        }

        public void Render()
        {
            Engine.Clear(0, 0, 0);
            Engine.Draw("Assets/floorStone.png");
            objectManager.RenderAll();
            uiManager.Render(gameTimer.GetFormattedTime());
            Engine.Show();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Juego");
        }

        
    }
}
