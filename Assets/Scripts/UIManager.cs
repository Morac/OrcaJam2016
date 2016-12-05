using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
	public Image Fadeout;
	public GameObject HighScoreDialogue;
	public InputField HighScoreSubmitDialogue;
	public Text HighScoreText;

	public GameObject CollectionUIRoot;
	public CanvasGroup CollectionUIBackground;
	public CollectionUIController CollectionUI;

	public Button CollectionButton;
	public Text FailureText;

	void Start()
	{
		Fadeout.color = new Color(Fadeout.color.r, Fadeout.color.g, Fadeout.color.b, 1);
		Fadeout.DOFade(0f, 0.5f);
		HighScoreDialogue.SetActive(false);

		CollectionUIRoot.SetActive(false);
		CollectionUIBackground.alpha = 0f;
		CollectionUI.transform.localScale = new Vector3(1, 0, 1);
	}

	public void DoFade(float duration = 2f)
	{
		Fadeout.DOFade(1f, duration).SetUpdate(true);
	}

	public void ShowHighScoreDialogue(string scoreText, System.Action<string> callback)
	{
		HighScoreDialogue.SetActive(true);
		HighScoreText.text = scoreText;
		HighScoreSubmitDialogue.onEndEdit.AddListener(playerName =>
		{
			HighScoreDialogue.SetActive(false);
			callback(playerName);
		});
	}

	public void ShowCollectionUI()
	{
		if (CollectionUIRoot.activeSelf)
			return;

		CollectionUIRoot.SetActive(true);
		CollectionUIBackground.DOFade(1, 0.5f);
		CollectionUI.transform.DOScale(1f, 0.5f).SetEase(Ease.OutQuad);

		GameManager.Instance.SetMenus(true);
	}

	public void HideCollectionUI()
	{
		if (CollectionUIRoot.activeSelf == false)
			return;

		CollectionUIBackground.DOFade(0, 0.5f);
		CollectionUI.transform.DOScale(new Vector3(1, 0, 1), 0.5f).SetEase(Ease.OutQuad)
			.OnComplete(() =>
			{
				CollectionUIRoot.SetActive(false);
				GameManager.Instance.SetMenus(false);
			});
	}

	public void HideCollectionButton()
	{
		CollectionButton.gameObject.SetActive(false);
	}
}
