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
        public event Action OnDied
        {
            add => health.OnDied += value;
            remove => health.OnDied -= value;
        }

        [SerializeField]
        private MatchData data;

        private IDamageable health;

        private TowerController controller;

        private void Awake()
        {
            health = new Health(data.TowerHealth, GetComponent<Collider>());
            controller = new(health, GetComponent<TowerView>());

            controller.Initialize();
        }

        public void TakeDamage(int damage) => health.TakeDamage(damage);

        private void OnDestroy()
        {
            controller.Dispose();
        }
    }
}