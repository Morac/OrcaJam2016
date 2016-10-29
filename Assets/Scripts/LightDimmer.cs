using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightDimmer : MonoBehaviour
{
	public float DimRate = 0.1f;

	float startY;
	float startAngle;
	Light theLight;

	void Start()
	{
		startY = transform.position.y;
		theLight = GetComponent<Light>();
		startAngle = theLight.spotAngle;
	}

	void Update()
	{
		var dist = Mathf.Abs(transform.position.y - startY);
		theLight.spotAngle = startAngle - dist * DimRate;
	}
}
