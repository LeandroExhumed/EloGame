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

        private IDamageable health;

        private TowerController controller;

        private void Awake()
        {
            Vector3 targetPosition = new(0f, UnityEngine.Random.Range(0.025f, 0.1f), 0f);
            health = new Health(20, GetComponent<Collider>());
            controller = new(health, GetComponent<TowerView>());
        }

        private void OnEnable()
        {
            controller.Initialize();
        }

        public void TakeDamage(int damage) => health.TakeDamage(damage);

        private void OnDestroy()
        {
            controller.Dispose();
        }
    }
}