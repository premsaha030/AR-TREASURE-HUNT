using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform target;   // PlayerTarget (moves with GPS)
    public float rotationSpeed = 5f;

    private Vector3 lastPosition;

    void Start()
    {
        if (target != null)
            lastPosition = target.position;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 movement = target.position - lastPosition;

        // Ignore very small movement (GPS noise)
        if (movement.magnitude > 0.5f)
        {
            Vector3 direction = new Vector3(movement.x, 0, movement.z);

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        lastPosition = target.position;
    }
}