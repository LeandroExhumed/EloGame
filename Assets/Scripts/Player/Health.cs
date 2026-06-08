using System;
using UnityEngine;

namespace DefaultCompany.Player
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

        public Health(int maxHealth)
        {
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth == 0)
            {
                OnDied?.Invoke(false);
            }
            else
            {
                OnDamageTaken?.Invoke();
            }
        }

        public void Restart()
        {
            CurrentHealth = maxHealth;
        }
    }
}