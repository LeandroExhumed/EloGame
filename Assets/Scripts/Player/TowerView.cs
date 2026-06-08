using DefaultCompany.Utils;
using DG.Tweening;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class TowerView : MonoBehaviour
    {
        [Header("Wind turbine fan")]
        [SerializeField]
        private Transform fan;
        [SerializeField]
        private float rotationSpeed = 2f;
        [SerializeField]
        private float fadeDuration = 3f;

        [Header("UI")]
        [SerializeField]
        private Gauge healthGauge;

        [Header("Pulse effect")]
        [SerializeField]
        private float punchStrength = 0.1f;
        [SerializeField]
        private float duration = 0.2f;
        [SerializeField]
        private int vibrato = 10;
        [SerializeField]
        private float elasticity = 1;

        [Header("SFX")]
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip damageSound;
        [SerializeField]
        private AudioClip deathSound;

        private Tween fanRotationTween;
        private Tween fadeTween;

        private void Awake()
        {
            PlayFanRotation();
        }

        public void PlayFanRotation()
        {
            if (fan == null)
            {
                return;
            }

            fanRotationTween?.Kill();

            fanRotationTween = fan.DORotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative(true);
        }

        public void SetHealthGauge(float currentHealth, float maxHealth)
        {
            // I would use D.I here but for this game is too much.
            if (healthGauge == null)
            {
                healthGauge = FindFirstObjectByType<Gauge>();
            }

            healthGauge?.UpdateGauge(currentHealth, maxHealth);
        }

        public void PlayPulseEffect()
        {
            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * punchStrength, duration, vibrato, elasticity).SetEase(Ease.OutCirc);
        }

        public void PlayDamageSound()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(damageSound);
        }

        public void Disable()
        {
            if (fan == null || fanRotationTween == null)
            {
                return;
            }

            fadeTween?.Kill();

            fadeTween = DOTween.To(() => fanRotationTween.timeScale, x => fanRotationTween.timeScale = x, 0f, fadeDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    fanRotationTween.Kill();
                });
        }

        public void PlayDeathSound()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound);
        }
    }
}