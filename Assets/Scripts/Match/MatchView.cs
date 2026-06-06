using TMPro;
using UnityEngine;

namespace DefaultCompany.Match
{
    public class MatchView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject gameplayUI;
        [SerializeField]
        private GameOverPanel gameoverPanel;

        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI countdownText;

        [Header("SFX")]
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip tickSound;

        public void SetGameplayUIActive(bool active) => gameplayUI.SetActive(active);

        public void SetScoreText(string text)
        {
            scoreText.text = text;
            gameoverPanel.SetScoreText(text);
        }

        public void SetCountdownText(string text)
        {
            countdownText.text = text;
        }

        public void PlayTickSound()
        {
            audioSource.PlayOneShot(tickSound);
        }

        public void OpenGameOverPanel()
        {
            gameoverPanel.Open();
        }
    }
}