using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CollectionUIEntry : MonoBehaviour
{
	public Text EntryName;
	public Text EntryDescription;
	public Image EntryImage;

	public Text HighScoreName;
	public Text HighScoreDepth;

	public Button NextEntryButton;
	public Button PrevEntryButton;

	public static CollectionUIEntry Current { get; private set; }

	CollectionUIEntry previousEntry;
	CollectionUIEntry nextEntry;

	public void Setup(CollectionManager.Entry entry, CollectionUIEntry prevEntry, CollectionUIEntry nextEntry)
	{
		EntryName.text = entry.Name;
		EntryDescription.text = entry.Description;
		EntryImage.sprite = entry.Image;

		SetName(entry.HighScoreName);
		SetDepth(entry.HighScoreDepth);

		this.previousEntry = prevEntry;
		this.nextEntry = nextEntry;

		DisableNavigationButtons();

		if (prevEntry != null)
		{
			PrevEntryButton.onClick.AddListener(() =>
			{
				DOTween.KillAll(transform.parent);
				transform.parent.DOLocalMoveX(-prevEntry.transform.localPosition.x, 0.5f).SetEase(Ease.OutQuad);
				DisableNavigationButtons();
				prevEntry.EnableNavigationBtns();
			});
		}
		else
		{
			PrevEntryButton.gameObject.SetActive(false);
		}

		if (nextEntry != null)
		{
			NextEntryButton.onClick.AddListener(() =>
			{
				DOTween.KillAll(transform.parent);
				transform.parent.DOLocalMoveX(-nextEntry.transform.localPosition.x, 0.5f).SetEase(Ease.OutQuad);
				DisableNavigationButtons();
				nextEntry.EnableNavigationBtns();
			});
		}
		else
		{
			NextEntryButton.gameObject.SetActive(false);
		}
	}

	public void SetName(string name)
	{
		HighScoreName.text = string.IsNullOrEmpty(name) ? "---" : name;
	}

	public void SetDepth(float depth)
	{
		HighScoreDepth.text = depth > 0 ? depth.ToString("0.0") + "m" : "---";
	}

	public void EnableNavigationBtns()
	{
		if (previousEntry != null)
		{
			PrevEntryButton.gameObject.SetActive(true);
		}

		if (nextEntry != null)
		{
			NextEntryButton.gameObject.SetActive(true);
		}

		Current = this;
	}

	public void DisableNavigationButtons()
	{
		NextEntryButton.gameObject.SetActive(false);
		PrevEntryButton.gameObject.SetActive(false);
	}

	public void CloseCollection()
	{
		UIManager.Instance.HideCollectionUI();
	}

}
