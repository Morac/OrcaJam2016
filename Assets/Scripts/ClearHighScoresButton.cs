using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClearHighScoresButton : MonoBehaviour
{
	Button button;

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
		if(!PlayerPrefs.HasKey(HighScoreManager.ScoreKey))
		{
			button.interactable = false;
		}
	}

	void OnClick()
	{
		//TODO: Confirm dialogue
		PlayerPrefs.DeleteAll();
		button.interactable = false;
	}
}
