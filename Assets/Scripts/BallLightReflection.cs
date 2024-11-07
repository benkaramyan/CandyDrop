using UnityEngine;

public class BallLightReflection : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Save the initial rotation of the reflection
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Keep the reflection's rotation constant
        transform.rotation = initialRotation;
    }
}
