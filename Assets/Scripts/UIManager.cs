using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
	public Image Fadeout;
	public Text HighScoreText;

	public GameObject CollectionUIRoot;
	public CanvasGroup CollectionUIBackground;
	public CollectionUIController CollectionUI;

	public Button CollectionButton;
	public Text FailureText;

	public HighScoreDialogue HighScoreDialogue;

	void Start()
	{
		Fadeout.color = new Color(Fadeout.color.r, Fadeout.color.g, Fadeout.color.b, 1);
		Fadeout.DOFade(0f, 0.5f);
		HighScoreDialogue.gameObject.SetActive(false);

		CollectionUIRoot.SetActive(false);
		CollectionUIBackground.alpha = 0f;
		CollectionUI.transform.localPosition = new Vector3(0, 2000, 0);
	}

	public void DoFade(float duration = 2f)
	{
		Fadeout.DOFade(1f, duration).SetUpdate(true);
	}
	
	public void ShowCollectionUI()
	{
		if (CollectionUIRoot.activeSelf)
			return;

		CollectionUIRoot.SetActive(true);
		CollectionUIBackground.DOFade(1, 0.5f);
		CollectionUI.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutQuad);

		GameManager.Instance.SetMenus(true);
	}

	public void HideCollectionUI()
	{
		if (CollectionUIRoot.activeSelf == false)
			return;

		CollectionUIBackground.DOFade(0, 0.5f);
		CollectionUI.transform.DOLocalMoveY(2000, 0.5f).SetEase(Ease.InQuad)
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
