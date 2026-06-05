using DG.Tweening;
using UnityEngine;

namespace DefaultCompany.Player
{
    public class TowerView : MonoBehaviour
    {
        [Header("Pulse effect on damage")]
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
            
        }

        public void PlayDeathSound()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound);
        }
    }
}