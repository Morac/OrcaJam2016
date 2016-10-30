using UnityEngine;

public class TeethInTheDarkness : MonoBehaviour
{
	public Transform Teeth;

	public float DistanceModifier = 0.1f;

	void Update()
	{
		var playerpos = GameManager.Instance.Player.transform.position;

		var dist = (transform.position - playerpos).sqrMagnitude;

		float scale = 1 / Mathf.Pow(dist, DistanceModifier);
		scale = Mathf.Clamp01(scale);
		Teeth.localScale = new Vector3(1, scale, 1);
	}
}
