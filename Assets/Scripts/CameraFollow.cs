using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public float DampAmount = 0.2f;
	Vector3 offset;

	Vector3 dampVelocity;

	void Start()
	{
		offset = transform.position - Target.position;
	}

	void Update()
	{
		transform.position = Vector3.SmoothDamp(transform.position, Target.transform.position + offset, ref dampVelocity, DampAmount);
	}
}
