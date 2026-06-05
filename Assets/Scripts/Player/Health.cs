using System;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class Health : IDamageable
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDied;

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
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth == 0)
            {
                collider.enabled = false;
                OnDied?.Invoke();
            }
        }
    }
}