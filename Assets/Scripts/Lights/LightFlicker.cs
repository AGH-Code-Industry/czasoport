using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour {
    public float minIntensity = 0.5f; // Minimum intensity of the light
    public float maxIntensity = 1.5f; // Maximum intensity of the light
    public float flickerSpeed = 1.0f; // Speed of the flickering

    private Light2D light2DComponent;
    private float baseIntensity;

    void Start() {
        light2DComponent = GetComponent<Light2D>();
        baseIntensity = light2DComponent.intensity; // Get the base intensity of the light
        StartFlickering(); // Start the flickering effect
    }

    void StartFlickering() {
        InvokeRepeating("Flicker", 0.0f, flickerSpeed); // Invoke the Flicker method repeatedly with the specified flickerSpeed
    }

    void Flicker() {
        float randomIntensity = Random.Range(minIntensity, maxIntensity); // Get a random intensity value within the specified range
        light2DComponent.intensity = baseIntensity * randomIntensity; // Set the light intensity to the random value
    }
}