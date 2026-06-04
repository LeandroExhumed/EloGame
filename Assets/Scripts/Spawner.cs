using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace DefaultCompany
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject charizardPrefab;
        private GameObject charizard;

        private ARTrackedImageManager imageManager;

        private void OnEnable()
        {
            imageManager = GetComponent<ARTrackedImageManager>();
            imageManager.trackablesChanged.AddListener(OnImageChanged);
        }

        private void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
        {
            for (int i = 0; i < eventArgs.added.Count; i++)
            {
                charizard = Instantiate(charizardPrefab, eventArgs.added[i].transform);
            }
        }
    }
}