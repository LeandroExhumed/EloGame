using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace DefaultCompany
{
    public class ARGameRoot : MonoBehaviour
    {
        [SerializeField]
        private ARTrackedImageManager imageManager;
        [SerializeField]
        private GameObject arObjectPrefab;
        [SerializeField]
        private GameObject nonARObjectPrefab;

        [Header("Debug")]
        [SerializeField]
        private TextMeshProUGUI trackText;
        [SerializeField]
        private TextMeshProUGUI resultText;

        private void OnEnable()
        {
            imageManager.trackablesChanged.AddListener(OnImageChanged);
        }

        private void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                Instantiate(arObjectPrefab, trackedImage.transform);
                StartCoroutine(SpawnNonARObjectDelayed());
                trackText.text = $"Image detected for the first time: {trackedImage.referenceImage.name}";
                UpdateTrackingStatus(trackedImage);
            }

            foreach (var trackedImage in eventArgs.updated)
            {
                UpdateTrackingStatus(trackedImage);
            }

            foreach (var _ in eventArgs.removed)
            {
                trackText.text = $"Image tracking reference removed completely";
            }
        }

        private void UpdateTrackingStatus(ARTrackedImage trackedImage)
        {
            string text = "";
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                text = $"{trackedImage.referenceImage.name} is currently actively tracked and visible.";
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                text = $"{trackedImage.referenceImage.name} has limited tracking (out of view or obscured).";
            }
            else
            {
                text = "None";
            }

            resultText.text = text;
        }

        private IEnumerator SpawnNonARObjectDelayed()
        {
            yield return new WaitForEndOfFrame();
            Instantiate(nonARObjectPrefab);
        }
    }
}