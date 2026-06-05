using Assets.Scripts.Enemy;
using System;

namespace DefaultCompany.Enemy
{
    public  class EnemyController : IDisposable
    {
        private readonly IDamageable health;

        private readonly EnemyView view;

        public EnemyController(IDamageable health, EnemyView view)
        {
            this.health = health;
            this.view = view;
        }

        public void Initialize()
        {
            health.OnHealthChanged += HandleHealthChanged;
            health.OnDied += HandleDied;
        }

        private void HandleHealthChanged(int currentHealth, int _)
        {
            if (currentHealth > 0)
            {
                view.PlayPulseEffect();
                view.PlayDamageSound();
            }
        }

        private void HandleDied()
        {
            view.Disable();
            view.PlayDeathSound();
        }

        public void Dispose()
        {
            health.OnDied -= HandleDied;
            health.OnHealthChanged -= HandleHealthChanged;
        }
    }
}