using UnityEngine;

public class Obstacle : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponent<FancyPlayerMover>();
		if(player)
		{
			player.HitObstacle(this);
		}
	}
	
}
