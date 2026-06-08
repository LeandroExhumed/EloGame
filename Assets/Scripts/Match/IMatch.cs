using System;

namespace DefaultCompany.Match
{
    public interface IMatch : ITickable, IDisposable
    {
        event Action<int> OnScoreChanged;
        event Action<float> OnCountdownChanged;
        event Action<int> OnGameOver;
        event Action OnRestart;

        void Initialize();
        void Restart();
    }
}