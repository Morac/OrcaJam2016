using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DepthLabel : MonoBehaviour
{
	public Transform Player;

	Text theText;

	void Start()
	{
		theText = GetComponent<Text>();
	}

	void Update()
	{
		if (Player.position.y > 0)
			theText.text = "";
		else
			theText.text = (-Player.position.y).ToString("0.0") + " m";
	}
}
