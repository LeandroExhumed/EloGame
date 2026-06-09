using DefaultCompany.Utils.Pooling;
using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField]
        private EnemyFacade enemyPrefab;
        [SerializeField]
        private Transform[] spawnPoints;
        [SerializeField]
        private int poolSize = 30;

        private IPool pool;

        private void Awake()
        {
            pool = new Pool();
            pool.AddPool(enemyPrefab, poolSize, transform);
        }

        public EnemyFacade GetEnemy()
        {
            int index = Random.Range(0, spawnPoints.Length);
            Vector3 position = spawnPoints[index].position;
            position.y += Random.Range(0, 0.08f);

            EnemyFacade enemy = pool.GetObject<EnemyFacade>(enemyPrefab);
            enemy.transform.SetPositionAndRotation(position,Quaternion.identity);
            enemy.Initialize();

            return enemy;
        }
    }
}