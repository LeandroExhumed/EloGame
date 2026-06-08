using System;
using UnityEngine;

public interface IDamageable
{
    event Action<int, int> OnHealthChanged;
    event Action OnDamageTaken;
    event Action<bool> OnDied;

    void Restart();
    void TakeDamage(int damage);
}
