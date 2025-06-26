using UnityEngine;
using UnityEngine.XR.ARFoundation;
namespace Sayan.ARFoundation
{
    public class PlaneLogger : MonoBehaviour
    {
        private ARPlaneManager planeManager;

        void Start()
        {
            planeManager = GetComponent<ARPlaneManager>();
            planeManager.planesChanged += OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            foreach (var plane in args.added)
            {
                Debug.Log("New Plane Detected: " + plane.trackableId);
            }
        }
    }
}