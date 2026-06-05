using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace DefaultCompany
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject charizardPrefab;
        [SerializeField]
        private TextMeshProUGUI trackText;
        [SerializeField]
        private TextMeshProUGUI resultText;
        private GameObject charizard;

        private ARTrackedImageManager imageManager;

        private void OnEnable()
        {
            imageManager = GetComponent<ARTrackedImageManager>();
            imageManager.trackablesChanged.AddListener(OnImageChanged);
        }

        private void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                charizard = Instantiate(charizardPrefab, trackedImage.transform);
                trackText.text = $"Image detected for the first time: {trackedImage.referenceImage.name}";
                UpdateTrackingStatus(trackedImage);
            }

            foreach (var trackedImage in eventArgs.updated)
            {
                UpdateTrackingStatus(trackedImage);
            }

            foreach (var trackedImage in eventArgs.removed)
            {
                trackText.text = $"Image tracking reference removed completely";
            }
        }
        private void UpdateTrackingStatus(ARTrackedImage trackedImage)
        {
            // Check the specific tracking health from ARCore
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                resultText.text = $"{trackedImage.referenceImage.name} is currently actively tracked and visible.";
                // Your custom logic: E.g., enable your 3D prefab, play a sound
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                resultText.text = $"{trackedImage.referenceImage.name} has limited tracking (out of view or obscured).";
                // Your custom logic: E.g., hide the 3D visual elements
            }
            else
            {
                resultText.text = "None";
            }
        }
    }
}