using UnityEngine;

public class GPSMovementSmooth : MonoBehaviour
{
    public Transform target; // PlayerTarget
    public float smoothTime = 5f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position,
            ref velocity,
            1f / smoothTime
        );
    }
}