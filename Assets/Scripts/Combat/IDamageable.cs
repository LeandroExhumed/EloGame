using System;
using UnityEngine;

public interface IDamageable
{
    event Action<int, int> OnHealthChanged;
    event Action OnDied;

    void TakeDamage(int damage);
}
