using System;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class EnemyFacade : MonoBehaviour, IDamageable
    {
        public event Action<int> OnHealthChanged
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

        private EnemyController controller;

        private void Awake()
        {
            health = new Health(2);
            controller = new(health, gameObject);
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