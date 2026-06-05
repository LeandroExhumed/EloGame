using System;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class Tower : IDamageable
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

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            OnDied?.Invoke();
        }
    }
}