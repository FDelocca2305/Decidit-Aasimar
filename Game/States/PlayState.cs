using Game.Core;
using Game.Scripts;
using Game.Scripts.Enemies;
using Game.Scripts.Waves;
using System;
using Game.Scripts.Utils;

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
        private bool bossSpawned = false;
        private Boss boss;
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
            if (!bossSpawned && gameTimer.TimeElapsedInMinutes >= .01f)
            {
                SpawnBoss();
            }

            if (!bossSpawned)
            {
                waveManager.Update(deltaTime);
            }
            else
            {
                boss.Update(deltaTime);
                uiManager.UpdateBossHealth(boss.CurrentHealth, boss.MaxHealth);
                if (boss.CurrentHealth <= 0)
                {
                    uiManager.HideBossBar();
                    GameManager.Instance.ChangeState(new VictoryState());
                }
            }

            if (player.Health <= 0)
            {
                GameManager.Instance.ChangeState(new GameOverState());
            }

            if (Engine.GetKey(Keys.ESCAPE))
                GameManager.Instance.ChangeState(new MainMenuState());

            uiManager.HandleUpgradeSelection();
            gameTimer.Update(deltaTime);
            objectManager.UpdateAll(deltaTime, player);
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

        private void SpawnBoss()
        {
            bossSpawned = true;
            objectManager.RemoveAllEnemies();
            boss = new Boss();
            boss.Initialize(1.5f);
            boss.OnDeath += HandleBossDefeat;
            boss.Reset(new Vector2(960,540), boss.MaxHealth, boss.Speed, player);
            uiManager.ShowBossBar(boss.CurrentHealth, boss.CurrentHealth);
            objectManager.Add(boss);
        }

        private void HandleBossDefeat(Enemy boss)
        {
            GameManager.Instance.ChangeState(new VictoryState());
        }
    }
}
