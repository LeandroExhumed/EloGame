using UnityEngine;
using UnityEngine.UI;

namespace DefaultCompany.UI
{
    public class Gauge : MonoBehaviour
    {
        [SerializeField]
        private Image fillableImage;

        public void UpdateGauge (float amountToFill, float maximumValue)
        {
            fillableImage.fillAmount = (float)amountToFill / maximumValue;
        }
    }
}