using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Sayan.ARFoundation
{
    public class ARPhysicsController : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private GameObject capsulePrefab;

        [Header("UI Elements")]
        [SerializeField] private TMP_Dropdown shapeDropdown;
        [SerializeField] private TMP_Dropdown planetDropdown;
        [SerializeField] private TMP_Text gravityValueText;
        [SerializeField] private TMP_Text velocityText;
        [SerializeField] private TMP_Text tutorialText;
        [SerializeField] private Slider gravitySlider;
        [SerializeField] private Button dropButton;
        [SerializeField] private Button resetButton;

        [Header("Spawn Settings")]
        [SerializeField] private Transform spawnPoint;

        private float gravity = -9.81f;
        private GameObject currentObject;
        private Rigidbody currentRb;
        private List<GameObject> spawnedObjects = new List<GameObject>();

        private void Start()
        {
            dropButton.onClick.AddListener(SpawnSelectedShape);
            resetButton.onClick.AddListener(ResetSimulation);
            planetDropdown.onValueChanged.AddListener(OnPlanetChanged);
            gravitySlider.onValueChanged.AddListener(OnGravitySliderChanged);

            OnPlanetChanged(planetDropdown.value);
            tutorialText.text = "Tilt the phone to affect falling direction. Tap 'Drop Shape' to see gravity in action.";

            // Enable gyroscope input (not mandatory but good)
            Input.gyro.enabled = true;
        }

        private void Update()
        {
            if (currentRb != null)
            {
                float speed = currentRb.linearVelocity.magnitude;
                velocityText.text = $"Velocity: {speed:F2} m/s";
            }

            // Update physics gravity direction based on tilt
            UpdateGravityDirection();
        }

        private void UpdateGravityDirection()
        {
            // Gravity direction based on device tilt
            Vector3 deviceTilt = Input.acceleration.normalized;
            Physics.gravity = deviceTilt * gravity;

            gravityValueText.text = $"Gravity: {Physics.gravity.ToString("F2")} m/s²";
        }

        private void SpawnSelectedShape()
        {
            GameObject prefab = GetSelectedPrefab();
            if (prefab == null || spawnPoint == null) return;

            GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            currentObject = obj;
            currentRb = obj.GetComponent<Rigidbody>();
            spawnedObjects.Add(obj);
        }

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

        private void OnPlanetChanged(int index)
        {
            switch (index)
            {
                case 0: gravity = -9.81f; break; // Earth
                case 1: gravity = -1.62f; break; // Moon
                case 2: gravity = -3.71f; break; // Mars
            }

            gravitySlider.value = -gravity;
        }

        private void OnGravitySliderChanged(float value)
        {
            gravity = -value;
        }

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
