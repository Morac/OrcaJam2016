using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClearHighScoresButton : MonoBehaviour
{

	void Start()
	{
		var btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		//TODO: Confirm dialogue
		PlayerPrefs.DeleteAll();
	}
}
