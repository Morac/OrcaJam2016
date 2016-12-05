using UnityEngine;
using UnityEngine.UI;

public class HighScoreDialogue : MonoBehaviour
{
	public InputField InputField;
	public Button SubmitButton;

	public void Show(System.Action<string> callback)
	{
		gameObject.SetActive(true);
		SubmitButton.onClick.AddListener(() =>
		{
			callback(InputField.text);
			gameObject.SetActive(false);
		});
	}

	void Update()
	{
		SubmitButton.interactable = InputField.text.Length > 0;
	}
	
}
