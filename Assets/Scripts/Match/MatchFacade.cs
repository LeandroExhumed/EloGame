using DefaultCompany.Enemy;
using DefaultCompany.Player;
using System;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchFacade : MonoBehaviour, IMatch
    {
        public event Action<int> OnScoreChanged
        {
            add => model.OnScoreChanged += value;
            remove => model.OnScoreChanged -= value;
        }
        public event Action<float> OnCountdownChanged
        {
            add => model.OnCountdownChanged += value;
            remove => model.OnCountdownChanged -= value;
        }
        public event Action<int> OnGameOver
        {
            add => model.OnGameOver += value;
            remove => model.OnGameOver -= value;
        }
        public event Action OnRestart
        {
            add => model.OnRestart += value;
            remove => model.OnRestart-= value;
        }

        [SerializeField]
        private MatchData data;

        [SerializeField]
        private TowerFacade tower;
        [SerializeField]
        private EnemyFactory enemyFactory;

        [SerializeField]
        private Camera mainCamera;

        private IMatch model;

        private IController controller;

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
            controller = new MatchController(model, GetComponent<MatchView>());

            controller.Initialize();
        }

        private void Start()
        {
            model.Initialize();
        }

        public void Initialize() => model.Initialize();

        public void Tick() => model.Tick();

        private void Update()
        {
            Tick();
        }

        public void Restart() => model.Restart();

        public void Dispose()
        {
            model.Dispose();
            controller.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}