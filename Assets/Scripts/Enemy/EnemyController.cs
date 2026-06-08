using Assets.Scripts.Enemy;
using System;

namespace DefaultCompany.Enemy
{
    public  class EnemyController : IController
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
            health.OnDamageTaken += HandleDamageTaken;
            health.OnDied += HandleDied;
        }

        private void HandleDamageTaken()
        {
            view.PlayPulseEffect(false);
            view.PlayDamageSound();
        }

        private void HandleDied(bool forced)
        {
            if (forced)
            {
                view.Disable();
            }
            else
            {
                view.PlayPulseEffect(true);
                view.PlayDeathSound();
            }
        }

        public void Dispose()
        {
            health.OnDamageTaken -= HandleDamageTaken;
            health.OnDied -= HandleDied;
        }
    }
}