using SpaceChaos.Utils;
using System;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchController : IDisposable
    {
        private const string HIGH_SCORE_DATA = "high_score.dat";
        private int lastSecondRegistered;

        private readonly IMatch model;

        private readonly MatchView view;

        public MatchController(IMatch model, MatchView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            model.OnScoreChanged += HandleScoreChanged;
            model.OnCountdownChanged += HandleCountdownChanged;
            model.OnGameOver += HandleGameOver;
            model.OnRestart += HandleRestart;
        }

        private void HandleGameOver(int currentScore)
        {
            view.SetGameplayUIActive(false);

            int bestScoreSaved = DataService.load<int>(HIGH_SCORE_DATA);

            if (bestScoreSaved != 0)
            {
                if (currentScore > bestScoreSaved)
                {
                    DataService.save(HIGH_SCORE_DATA, currentScore);
                    bestScoreSaved = currentScore;
                }
            }
            else
            {
                DataService.save(HIGH_SCORE_DATA, currentScore);
                bestScoreSaved = currentScore;
            }

            view.OpenGameOverPanel(currentScore, bestScoreSaved);
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

        private void HandleRestart()
        {
            view.SetGameplayUIActive(true);
        }

        public void Dispose()
        {
            model.OnScoreChanged -= HandleScoreChanged;
            model.OnCountdownChanged -= HandleCountdownChanged;
            model.OnGameOver -= HandleGameOver;
            model.OnRestart -= HandleRestart;
        }
    }
}