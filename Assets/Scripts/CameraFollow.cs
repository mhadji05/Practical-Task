using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform PlayerTransform;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - PlayerTransform.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCameraPos = PlayerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position,
            targetCameraPos, smoothing * Time.deltaTime);
    }

}