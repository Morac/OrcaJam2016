using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
	public Image Fadeout;
	public GameObject HighScoreDialogue;
	public InputField HighScoreSubmitDialogue;
	public Text HighScoreText;

	void Start()
	{
		Fadeout.color = new Color(Fadeout.color.r, Fadeout.color.g, Fadeout.color.b, 1);
		Fadeout.DOFade(0f, 0.5f);
		HighScoreDialogue.SetActive(false);
	}

	public void DoFade()
	{
		Fadeout.DOFade(1f, 2f).SetUpdate(true);
	}

	public void ShowHighScoreDialogue(string scoreText, System.Action<string> callback)
	{
		HighScoreDialogue.SetActive(true);
		HighScoreText.text = scoreText;
		HighScoreSubmitDialogue.onEndEdit.AddListener(s =>
		{
			HighScoreDialogue.SetActive(false);
			callback(s);
		});
	}
}
