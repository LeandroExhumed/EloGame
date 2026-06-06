using System;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchController : IDisposable
    {
        private int lastSecondRegistered;

        private readonly MatchModel model;

        private readonly MatchView view;

        public MatchController(MatchModel model, MatchView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            model.OnScoreChanged += HandleScoreChanged;
            model.OnCountdownChanged += HandleCountdownChanged;
            model.OnGameOver += HandleGameOver;
        }

        private void HandleGameOver()
        {
            view.SetGameplayUIActive(false);
        }

        private void HandleScoreChanged(int currentScore)
        {
            view.SetScoreText(currentScore.ToString());
        }

        private void HandleCountdownChanged(float counter)
        {
            int currentSecond = Mathf.CeilToInt(counter);
            view.SetCountdownText(currentSecond.ToString());

            if (lastSecondRegistered == 0f)
            {
                lastSecondRegistered = currentSecond;
            }

            if (currentSecond < lastSecondRegistered && currentSecond <= 10 && currentSecond > 0)
            {
                lastSecondRegistered = currentSecond;

                view.PlayTickSound();
            }
        }

        public void Dispose()
        {
            model.OnScoreChanged -= HandleScoreChanged;
            model.OnCountdownChanged -= HandleCountdownChanged;
        }
    }
}