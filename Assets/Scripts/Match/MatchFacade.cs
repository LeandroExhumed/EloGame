using DefaultCompany.Enemy;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchFacade : MonoBehaviour
    {
        [SerializeField]
        private EnemyFacade[] enemies;

        private MatchModel model;

        private MatchController controller;

        private void Awake()
        {
            model = new MatchModel(enemies);
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