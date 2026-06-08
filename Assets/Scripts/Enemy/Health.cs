using System;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class Health : IDamageable
    {
        public event Action<int, int> OnHealthChanged;
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

        public Health(int maxHealth)
        {
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            bool forced = damage == int.MaxValue;
            CurrentHealth = forced ? 0 : Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth == 0)
            {
                OnDied?.Invoke(forced);
            }
        }

        public void Restart()
        {
            CurrentHealth = maxHealth;
        }
    }
}