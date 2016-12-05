using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HighScoreDialogue : MonoBehaviour
{
	public InputField InputField;
	public Button SubmitButton;

	public void Show(System.Action<string> callback)
	{
		transform.localScale = Vector3.zero;
		transform.DOScale(1, 0.5f).SetEase(Ease.OutQuad);
		gameObject.SetActive(true);
		SubmitButton.onClick.AddListener(() =>
		{
			callback(InputField.text);
			transform.DOScale(0, 0.5f).SetEase(Ease.InQuad).OnComplete(() => gameObject.SetActive(false));
		});
	}

	void Update()
	{
		SubmitButton.interactable = InputField.text.Length > 0;
	}
	
}
