using System;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class TowerController : IDisposable
    {
        private readonly IDamageable health;

        private readonly TowerView view;

        public TowerController(IDamageable health, TowerView view)
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
            health.OnHealthChanged -= HandleHealthChanged;
            health.OnDied -= HandleDied;
        }
    }
}