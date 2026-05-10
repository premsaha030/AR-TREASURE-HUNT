using UnityEngine;

public class CameraCompassRotation : MonoBehaviour
{
    public Transform target; // PlayerTarget
    public float distance = 15f;
    public float height = 10f;
    public float smoothSpeed = 5f;

    void Start()
    {
        // Enable compass
        Input.compass.enabled = true;

        // Start GPS (required for compass)
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float heading = Input.compass.trueHeading;

        // Convert heading to rotation
        Quaternion rotation = Quaternion.Euler(0, heading, 0);

        // Position camera behind player based on compass
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        Vector3 desiredPosition = target.position + offset;

        // Smooth movement
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Always look at player
        transform.LookAt(target.position + Vector3.up * 2f);
    }
}