using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    public Transform target;

    public float distance = 15f;
    public float height = 10f;

    public float minDistance = 5f;
    public float maxDistance = 25f;

    public float minTilt = 20f;
    public float maxTilt = 70f;

    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 0.1f;
    public float tiltSpeed = 0.2f;

    public float smoothSpeed = 5f;

    private float currentAngle = 0f;
    private float currentTilt = 45f;

    void Update()
    {
        // ? SINGLE TOUCH (ROTATE + TILT)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                currentAngle += touch.deltaPosition.x * rotationSpeed;

                currentTilt -= touch.deltaPosition.y * tiltSpeed;
                currentTilt = Mathf.Clamp(currentTilt, minTilt, maxTilt);
            }
        }

        // ? PINCH ZOOM
        if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            float prevDist = (t1.position - t1.deltaPosition - (t2.position - t2.deltaPosition)).magnitude;
            float currentDist = (t1.position - t2.position).magnitude;

            float difference = currentDist - prevDist;

            distance -= difference * zoomSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(currentTilt, currentAngle, 0);

        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 2f);
    }

    // ? SNAP TO NORTH BUTTON
    public void SnapToNorth()
    {
        currentAngle = 0f;
    }
}