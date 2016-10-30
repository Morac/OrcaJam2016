using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boat : Singleton<Boat>
{
	public Transform Player;
	public Transform FishingLine;
	public Transform FishingLineMountPoint;

	public float positionDamp = 1;
	public float positionOffset = 4;

	float posDampVelocity;

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		var x = Mathf.SmoothDamp(transform.position.x, Player.position.x + positionOffset, ref posDampVelocity, positionDamp);

		transform.position = new Vector3(x, transform.position.y, transform.position.z);

		var mid = Player.position - FishingLineMountPoint.position;
		mid /= 2;
		mid = FishingLineMountPoint.position + mid;

		FishingLine.position = mid;

		float scale = (Player.position - FishingLineMountPoint.position).magnitude * 9;
		FishingLine.localScale = new Vector3(FishingLine.localScale.x, scale, FishingLine.localScale.z);

		FishingLine.up = (FishingLineMountPoint.position - Player.position).normalized;
	}

	public void PlayEndAnimation()
	{
		anim.SetTrigger("TriggerEnd");
	}
}
