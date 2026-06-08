using DefaultCompany.Enemy;
using DefaultCompany.Player;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchFacade : MonoBehaviour
    {
        [SerializeField]
        private MatchData data;

        [SerializeField]
        private TowerFacade tower;
        [SerializeField]
        private EnemyFactory enemyFactory;

        [SerializeField]
        private Camera mainCamera;

        private MatchModel model;

        private MatchController controller;

        private void Awake()
        {
            // I would use D.I here but for this game is too much.
            if (tower == null)
            {
                tower = FindFirstObjectByType<TowerFacade>();
            }
            if (enemyFactory == null)
            {
                enemyFactory = FindFirstObjectByType<EnemyFactory>();
            }
            if (mainCamera == null)
            {
                mainCamera = FindFirstObjectByType<Camera>();
            }

            model = new MatchModel(data, new InputRunner(data.PlayerDamage, mainCamera), enemyFactory, tower);
            controller = new(model, GetComponent<MatchView>());

            controller.Initialize();
        }

        private void Start()
        {
            model.Initialize();
        }

        private void Update()
        {
            model.Tick();
        }

        private void OnDestroy()
        {
            model.Dispose();
            controller.Dispose();
        }
    }
}