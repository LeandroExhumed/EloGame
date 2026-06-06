using DefaultCompany.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchModel : ITickable, IDisposable
    {
        public event Action<int> OnScoreChanged;
        public event Action<float> OnCountdownChanged;
        public event Action OnGameOver;

        private int CurrentScore
        {
            get => currentScore;
            set
            {
                currentScore = value;
                OnScoreChanged?.Invoke(value);
            }
        }
        private float Countdown
        {
            get => countdown;
            set
            {
                countdown = value;
                OnCountdownChanged?.Invoke(value);
            }
        }

        private MatchData data;

        private bool isOver = false;

        private int currentScore = 0;

        private float countdown;

        private float spawnCounter = 0f;
        private readonly List<EnemyFacade> enemies = new();

        private readonly InputRunner inputRunner;
        private readonly EnemyFactory enemyFactory;
        private readonly IDamageable tower;

        public MatchModel(MatchData data, InputRunner inputRunner, EnemyFactory enemyFactory, IDamageable tower)
        {
            this.data = data;
            this.inputRunner = inputRunner;
            this.enemyFactory = enemyFactory;
            this.tower = tower;
        }

        public void Initialize()
        {
            CurrentScore = 0;
            Countdown = data.Duration;

            tower.OnDied += HandleTowerDied;
        }

        public void Tick()
        {
            if (isOver)
            {
                return;
            }

            inputRunner.Tick();

            if (Countdown <= 0f)
            {
                FinishGame();
            }
            else
            {
                Countdown -= Time.deltaTime;
            }

            if (spawnCounter >= data.SpawnRate)
            {
                SpawnEnemy();
                spawnCounter = 0f;
            }
            else
            {
                spawnCounter += Time.deltaTime;
            }
        }

        private void SpawnEnemy()
        {
            EnemyFacade enemy = enemyFactory.GetEnemy();
            enemies.Add(enemy);
            enemy.OnDied += HandleEnemyDied;
        }

        private void FinishGame()
        {
            isOver = true;
            OnGameOver?.Invoke();
        }

        private void HandleTowerDied()
        {
            FinishGame();
        }

        private void HandleEnemyDied()
        {
            CurrentScore++;
        }

        public void Dispose()
        {
            tower.OnDied -= HandleTowerDied;
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].OnDied -= HandleEnemyDied;
            }
        }
    }
}