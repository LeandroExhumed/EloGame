using UnityEngine;

namespace DefaultCompany.Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        public int MaxHealth => maxHealth;
        public float Speed => speed;
        public int AttackDamage => attackDamage;
        public float DistanceToAttack => distanceToAttack;
        public float AttackRate => attackRate;

        [SerializeField]
        private int maxHealth = 3;
        [SerializeField]
        private float speed = 0.03f;
        [SerializeField]
        private int attackDamage = 1;
        [SerializeField]
        private float distanceToAttack = 0.02f;
        [SerializeField]
        private float attackRate = 1f;
    }
}