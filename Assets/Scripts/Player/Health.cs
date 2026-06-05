using System;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class Health : IDamageable
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDied;

        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                OnHealthChanged?.Invoke(value);
            }
        }

        private int currentHealth;

        public Health(int initialHealth)
        {
            CurrentHealth = initialHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth == 0)
            {
                OnDied?.Invoke();
            }
        }
    }
}