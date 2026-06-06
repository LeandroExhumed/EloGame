using UnityEngine;

namespace DefaultCompany.Match
{
    [CreateAssetMenu(fileName = "Match", menuName = "Data/Match")]
    public class MatchData : ScriptableObject
    {
        public float Duration => duration;
        public float SpawnRate => spawnRate;

        public int PlayerDamage => playerDamage;
        public int TowerHealth => towerHealth;

        [SerializeField]
        private float duration = 60f;
        [SerializeField]
        private float spawnRate = 2f;

        [Header("Player/Tower")]
        [SerializeField]
        private int playerDamage = 1;
        [SerializeField]
        private int towerHealth = 20;
    }
}