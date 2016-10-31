using UnityEngine;

public class TargetHitArea : MonoBehaviour
{
	Target target;

	void Start()
	{
		target = GetComponentInParent<Target>();
	}

	void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponent<FancyPlayerMover>();
		if (player)
		{
			player.AttachTo(target);
			transform.parent.localPosition = Vector3.zero;
			transform.parent.RotateAround(transform.position, new Vector3(0, 0, 1), -90);
		}
	}
}
