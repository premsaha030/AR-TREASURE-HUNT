using UnityEngine;

public class PlayerCompassRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Start()
    {
        Input.compass.enabled = true;

        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }
    }

    void Update()
    {
        float heading = Input.compass.trueHeading;

        Quaternion targetRotation = Quaternion.Euler(0, heading, 0);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}