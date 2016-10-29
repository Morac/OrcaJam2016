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
		}
	}
}
