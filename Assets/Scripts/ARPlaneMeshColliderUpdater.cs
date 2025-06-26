using UnityEngine;
using UnityEngine.XR.ARFoundation;
namespace Sayan.ARFoundation
{
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(ARPlaneMeshVisualizer))]
    public class ARPlaneMeshColliderUpdater : MonoBehaviour
    {
        private MeshCollider meshCollider;
        private ARPlaneMeshVisualizer meshVisualizer;

        void Awake()
        {
            meshCollider = GetComponent<MeshCollider>();
            meshVisualizer = GetComponent<ARPlaneMeshVisualizer>();
        }

        void Update()
        {
            meshCollider.sharedMesh = meshVisualizer.mesh;
        }
    }
}