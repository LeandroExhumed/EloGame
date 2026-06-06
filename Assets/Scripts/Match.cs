using DefaultCompany.Enemy;
using DefaultCompany.Player;
using DefaultCompany.UI;
using System;
using TMPro;
using UnityEngine;

namespace DefaultCompany
{
    public class Match : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private EnemyFacade[] enemies;

        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI countdownText;

        [SerializeField]
        private Gauge healthGauge;

        [SerializeField]
        private TowerFacade tower;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip tickSound;

        private bool isOver = false;

        private int currentScore = 0;

        private readonly float matchDuration = 60f;
        private float counter = 0f;
        private int lastSecondRegistered;

        private void Awake()
        {
            counter = matchDuration;
            lastSecondRegistered = Mathf.CeilToInt(counter);
        }

        private void Start()
        {
            foreach (var enemy in enemies)
            {
                enemy.OnDied += HandleEnemyDied;
            }
            tower.OnHealthChanged += HandleTowerHealthChanged;
        }

        private void Update()
        {
            if (!isOver && counter <= 0f)
            {
                isOver = true;
                countdownText.gameObject.SetActive(false);
            }
            else
            {
                counter -= Time.deltaTime;

                DisplayCountdown(Mathf.CeilToInt(counter).ToString());
                ApplyAudioFeedback();
            }
        }

        private void DisplayCountdown(string text)
        {
            countdownText.text = text;
        }

        private void ApplyAudioFeedback()
        {
            int currentSecond = Mathf.CeilToInt(counter);

            if (currentSecond < lastSecondRegistered && currentSecond <= 10 && currentSecond > 0)
            {
                lastSecondRegistered = currentSecond;

                audioSource.PlayOneShot(tickSound);
            }
        }

        private void HandleTowerHealthChanged(int currentHealth, int maxHealth)
        {
            healthGauge.UpdateGauge(currentHealth, maxHealth);
        }

        private void HandleEnemyDied()
        {
            currentScore++;
            scoreText.text = currentScore.ToString();
        }

        private void OnDestroy()
        {
            tower.OnHealthChanged -= HandleTowerHealthChanged;
        }

        public void Dispose()
        {
            foreach (var enemy in enemies)
            {
                enemy.OnDied -= HandleEnemyDied;
            }
        }
    }
}