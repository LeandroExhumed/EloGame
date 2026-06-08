using System;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class Health : IDamageable
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDamageTaken;
        public event Action<bool> OnDied;

        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                OnHealthChanged?.Invoke(value, maxHealth);
            }
        }

        private readonly int maxHealth;
        private int currentHealth;

        private readonly Collider collider;

        public Health(int maxHealth, Collider collider)
        {
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
            this.collider = collider;
        }

        public void TakeDamage(int damage)
        {
            bool forced = damage == int.MaxValue;
            CurrentHealth = forced ? 0 : Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth == 0)
            {
                collider.enabled = false;
                OnDied?.Invoke(forced);
            }
            else
            {
                OnDamageTaken?.Invoke();
            }
        }

        public void Restart()
        {
            CurrentHealth = maxHealth;
            collider.enabled = true;
        }
    }
}