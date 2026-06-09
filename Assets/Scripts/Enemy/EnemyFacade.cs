using Assets.Scripts.Enemy;
using DefaultCompany.Player;
using DefaultCompany.Utils.Pooling;
using System;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class EnemyFacade : MonoBehaviour, IDamageable, IPoolable
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
        private EnemyData data;

        private ITickable movement;
        private IDamageable health;

        private IController controller;

        private void Awake()
        {
            // I would use D.I here but for this game is too much.
            Transform target = FindFirstObjectByType<TowerFacade>().transform;
            movement = new Movement(data, transform, target);
            health = new Health(data.MaxHealth, GetComponent<Collider>());
            controller = new EnemyController(health, GetComponent<EnemyView>());

            controller.Initialize();
        }

        private void Update()
        {
            movement.Tick();
        }

        public void TakeDamage(int damage) => health.TakeDamage(damage);

        public void Restart() => health.Restart();

        public void Reuse() => Restart();

        private void OnDestroy()
        {
            controller.Dispose();
        }
    }
}