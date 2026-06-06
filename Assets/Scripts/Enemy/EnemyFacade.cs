using Assets.Scripts.Enemy;
using System;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class EnemyFacade : MonoBehaviour, IDamageable
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

        private ITickable movement;
        private IDamageable health;

        private EnemyController controller;

        private void Awake()
        {
            Vector3 targetPosition = new(0f, UnityEngine.Random.Range(0.025f, 0.1f), 0f);
            movement = new Movement(0.05f, 1, 1f, transform, targetPosition);
            health = new Health(2);
            controller = new(health, GetComponent<EnemyView>());

            controller.Initialize();
        }

        private void Update()
        {
            movement.Tick();
        }

        public void TakeDamage(int damage) => health.TakeDamage(damage);

        private void OnDestroy()
        {
            controller.Dispose();
        }
    }
}