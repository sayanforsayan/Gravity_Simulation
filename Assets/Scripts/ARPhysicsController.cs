using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Sayan.ARFoundation
{
    public class ARPhysicsController : MonoBehaviour
    {
        // Prefabs for different shapes
        [Header("Prefabs")]
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private GameObject capsulePrefab;

        // UI controls for shape/planet selection and feedback
        [Header("UI Elements")]
        [SerializeField] private TMP_Dropdown shapeDropdown;
        [SerializeField] private TMP_Dropdown planetDropdown;
        [SerializeField] private TMP_Text gravityValueText;
        [SerializeField] private TMP_Text velocityText;
        [SerializeField] private TMP_Text tutorialText;
        [SerializeField] private Slider gravitySlider;
        [SerializeField] private Button dropButton;
        [SerializeField] private Button resetButton;

        // Spawn point where object will appear
        [Header("Spawn Settings")]
        [SerializeField] private Transform spawnPoint;

        // Gravity value (default Earth gravity)
        private float gravity = -9.81f;

        // Currently spawned object and its Rigidbody
        private GameObject currentObject;
        private Rigidbody currentRb;

        // List to keep track of all spawned shapes (for reset)
        private List<GameObject> spawnedObjects = new List<GameObject>();

        private void Start()
        {
            // Connect UI button events
            dropButton.onClick.AddListener(SpawnSelectedShape);
            resetButton.onClick.AddListener(ResetSimulation);
            planetDropdown.onValueChanged.AddListener(OnPlanetChanged);
            gravitySlider.onValueChanged.AddListener(OnGravitySliderChanged);

            // Set initial planet gravity and tutorial
            OnPlanetChanged(planetDropdown.value);
            tutorialText.text = "Tilt the phone to affect falling direction. Tap 'Drop Shape' to see gravity in action.";

            // Optional: enable gyroscope (not required for Input.acceleration)
            Input.gyro.enabled = true;
        }

        private void Update()
        {
            // If an object is falling, show its current velocity
            if (currentRb != null)
            {
                float speed = currentRb.linearVelocity.magnitude;
                velocityText.text = $"Velocity: {speed:F2} m/s";
            }

            // Update gravity direction based on device tilt
            UpdateGravityDirection();
        }

        // This updates Unity's Physics.gravity based on phone orientation
        private void UpdateGravityDirection()
        {
            // Get normalized tilt from phone's accelerometer
            Vector3 deviceTilt = Input.acceleration.normalized;

            // Apply gravity in the direction the phone is tilted
            Physics.gravity = deviceTilt * gravity;

            // Show actual gravity vector in UI
            gravityValueText.text = $"Gravity: {Physics.gravity.ToString("F2")} m/s²";
        }

        // Spawn the selected shape at the spawn point
        private void SpawnSelectedShape()
        {
            GameObject prefab = GetSelectedPrefab();
            if (prefab == null || spawnPoint == null) return;

            // Create instance and track it
            GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            currentObject = obj;
            currentRb = obj.GetComponent<Rigidbody>();
            spawnedObjects.Add(obj);
        }

        // Get which shape is selected in dropdown
        private GameObject GetSelectedPrefab()
        {
            switch (shapeDropdown.value)
            {
                case 0: return ballPrefab;
                case 1: return cubePrefab;
                case 2: return capsulePrefab;
                default: return ballPrefab;
            }
        }

        // When planet is changed, set corresponding gravity
        private void OnPlanetChanged(int index)
        {
            switch (index)
            {
                case 0: gravity = -9.81f; break; // Earth
                case 1: gravity = -1.62f; break; // Moon
                case 2: gravity = -3.71f; break; // Mars
            }

            // Sync gravity slider visually
            gravitySlider.value = -gravity;
        }

        // When slider is changed manually, update gravity
        private void OnGravitySliderChanged(float value)
        {
            gravity = -value;
        }

        // Destroys all shapes and clears velocity
        private void ResetSimulation()
        {
            foreach (var obj in spawnedObjects)
            {
                if (obj != null) Destroy(obj);
            }

            spawnedObjects.Clear();
            currentObject = null;
            currentRb = null;
            velocityText.text = "Velocity: 0.00 m/s";
        }

        // For visualizing spawn point in Unity Scene view
        private void OnDrawGizmos()
        {
            if (spawnPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(spawnPoint.position, 0.02f);
            }
        }
    }
}
