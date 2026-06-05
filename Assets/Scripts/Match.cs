using DefaultCompany.Player;
using UnityEngine;

namespace DefaultCompany
{
    public class Match : MonoBehaviour
    {
        [SerializeField]
        private GameObject healthGauge;

        [SerializeField]
        private TowerFacade tower;

        private void Start()
        {
            tower.OnHealthChanged += HandleTowerHealthChanged;
        }

        private void HandleTowerHealthChanged(int currentHealth)
        {
            Debug.Log("Tower health: " +  currentHealth);
        }

        private void OnDestroy()
        {
            tower.OnHealthChanged -= HandleTowerHealthChanged;
        }
    }
}