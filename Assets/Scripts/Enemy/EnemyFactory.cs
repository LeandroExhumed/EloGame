using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField]
        private EnemyFacade enemyPrefab;
        [SerializeField]
        private Transform[] spawnPoints;

        public EnemyFacade GetEnemy()
        {
            int index = Random.Range(0, spawnPoints.Length);
            Vector3 position = spawnPoints[index].position;
            position.y += Random.Range(0, 0.08f);
            return Instantiate(enemyPrefab, position, Quaternion.identity, transform);
        }
    }
}