using DefaultCompany.Match;
using System;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class TowerFacade : MonoBehaviour, IDamageable
    {
        public event Action<int, int> OnHealthChanged
        {
            add => health.OnHealthChanged += value;
            remove => health.OnHealthChanged -= value;
        }
        public event Action OnDamageTaken
        {
            add => health.OnDamageTaken += value;
            remove => health.OnDamageTaken -= value;
        }
        public event Action<bool> OnDied
        {
            add => health.OnDied += value;
            remove => health.OnDied -= value;
        }

        [SerializeField]
        private MatchData data;

        private IDamageable health;

        private IController controller;

        private void Awake()
        {
            health = new HealthModel(data.TowerHealth);
            controller = new TowerController(health, GetComponent<TowerView>());

            controller.Initialize();
        }

        public void TakeDamage(int damage) => health.TakeDamage(damage);

        public void Restart() => health.Restart();

        private void OnDestroy()
        {
            controller.Dispose();
        }
    }
}