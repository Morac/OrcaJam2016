using UnityEngine;

public class Target : MonoBehaviour
{
	public float ScaleFactor = 0.1f;
	public float Size { get; private set; }

	void Start()
	{
		Size = transform.localScale.x + Mathf.Abs(transform.position.y)* ScaleFactor;
		transform.localScale = new Vector3(Size, Size, Size);
	}

}
