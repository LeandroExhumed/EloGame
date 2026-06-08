using System;
using UnityEngine;

namespace DefaultCompany
{
    public interface IDamageable
    {
        event Action<int, int> OnHealthChanged;
        event Action OnDamageTaken;
        event Action<bool> OnDied;

        void Restart();
        void TakeDamage(int damage);
    }
}