using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Игрок
    public Vector3 offset;          // Смещение камеры
    public float smoothSpeed = 0.125f; // Плавность

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}

