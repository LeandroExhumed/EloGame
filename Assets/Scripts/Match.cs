using DefaultCompany.Player;
using DefaultCompany.UI;
using UnityEngine;

namespace DefaultCompany
{
    public class Match : MonoBehaviour
    {
        [SerializeField]
        private Gauge healthGauge;

        [SerializeField]
        private TowerFacade tower;

        private void Start()
        {
            tower.OnHealthChanged += HandleTowerHealthChanged;
        }

        private void HandleTowerHealthChanged(int currentHealth, int maxHealth)
        {
            healthGauge.UpdateGauge(currentHealth, maxHealth);
        }

        private void OnDestroy()
        {
            tower.OnHealthChanged -= HandleTowerHealthChanged;
        }
    }
}