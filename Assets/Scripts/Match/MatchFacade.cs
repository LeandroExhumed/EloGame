using DefaultCompany.Enemy;
using DefaultCompany.Player;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchFacade : MonoBehaviour
    {
        [SerializeField]
        private TowerFacade tower;
        [SerializeField]
        private EnemyFacade[] enemies;

        private MatchModel model;

        private MatchController controller;

        private void Awake()
        {
            model = new MatchModel(tower, enemies);
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