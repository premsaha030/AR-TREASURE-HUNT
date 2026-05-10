using UnityEngine;

public class CameraTouchRotate : MonoBehaviour
{
    public Transform target;
    public float distance = 15f;
    public float height = 10f;
    public float rotationSpeed = 0.2f;
    public float smoothSpeed = 5f;

    private float currentAngle = 0f;

    void Update()
    {
        // Touch input
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                currentAngle += touch.deltaPosition.x * rotationSpeed;
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);

        Vector3 offset = rotation * new Vector3(0, height, -distance);
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 2f);
    }
}