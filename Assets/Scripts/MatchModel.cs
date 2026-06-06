using DefaultCompany.Enemy;
using System;
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

        private bool isOver = false;

        private int currentScore = 0;

        private readonly float matchDuration = 20f;
        private float countdown = 0f;

        private readonly IDamageable tower;
        private readonly EnemyFacade[] enemies;

        public MatchModel(IDamageable tower, EnemyFacade[] enemies)
        {
            this.tower = tower;
            this.enemies = enemies;
        }

        public void Initialize()
        {
            CurrentScore = 0;
            Countdown = matchDuration;

            tower.OnDied += HandleTowerDied;
            foreach (var enemy in enemies)
            {
                enemy.OnDied += HandleEnemyDied;
            }
        }

        public void Tick()
        {
            if (isOver)
            {
                return;
            }

            if (Countdown <= 0f)
            {
                FinishGame();
            }
            else
            {
                Countdown -= Time.deltaTime;
            }
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
            foreach (var enemy in enemies)
            {
                tower.OnDied -= HandleTowerDied;
                enemy.OnDied -= HandleEnemyDied;
            }
        }
    }
}