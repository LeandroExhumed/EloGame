using System;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public  class EnemyController : IDisposable
    {
        private readonly IDamageable health;

        private readonly GameObject view;

        public EnemyController(IDamageable health, GameObject view)
        {
            this.health = health;
            this.view = view;
        }

        public void Initialize()
        {
            health.OnDied += HandleDied;
        }

        private void HandleDied()
        {
            view.SetActive(false);
        }

        public void Dispose()
        {
            health.OnDied -= HandleDied;
        }
    }
}