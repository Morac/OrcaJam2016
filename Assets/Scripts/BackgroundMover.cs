using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public Transform Cam;
    float MaxY;

    public float DampAmount = 0.3f;
    Vector3 velocity;

    void Start()
    {
        MaxY = transform.position.y;
    }

    void Update()
    {
        float z = transform.position.z;
        float y = Mathf.Min(MaxY, Cam.position.y);
        float x = Cam.position.x;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x, y, z), ref velocity, DampAmount);
    }
}
