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

	CollectionUIEntry previousEntry;
	CollectionUIEntry nextEntry;

	public void Setup(CollectionManager.Entry entry, CollectionUIEntry prevEntry, CollectionUIEntry nextEntry)
	{
		EntryName.text = entry.Name;
		EntryDescription.text = entry.Description;
		EntryImage.sprite = entry.Image;

		HighScoreName.text = entry.HighScoreName;
		HighScoreDepth.text = entry.HighScoreDepth > 0 ? entry.HighScoreDepth.ToString("0.0") + "m" : "";

		this.previousEntry = prevEntry;
		this.nextEntry = nextEntry;

		PrevEntryButton.gameObject.SetActive(false);
		NextEntryButton.gameObject.SetActive(false);

		if(prevEntry != null)
		{
			PrevEntryButton.onClick.AddListener(() =>
			{
				DOTween.KillAll(transform.parent);
				var layoutElement = GetComponent<LayoutElement>();
				transform.parent.DOMoveX(transform.parent.position.x - layoutElement.preferredWidth, 0.5f).SetEase(Ease.OutCirc);
				PrevEntryButton.gameObject.SetActive(false);
				NextEntryButton.gameObject.SetActive(false);
				prevEntry.EnableNavigationBtns();
			});
		}
		else
		{
			PrevEntryButton.gameObject.SetActive(false);
		}

		if(nextEntry != null)
		{
			NextEntryButton.onClick.AddListener(() =>
			{
				DOTween.KillAll(transform.parent);
				var layoutElement = GetComponent<LayoutElement>();
				transform.parent.DOMoveX(transform.parent.position.x + layoutElement.preferredWidth, 0.5f).SetEase(Ease.OutCirc);
				PrevEntryButton.gameObject.SetActive(false);
				NextEntryButton.gameObject.SetActive(false);
				nextEntry.EnableNavigationBtns();
			});
		}
		else
		{
			NextEntryButton.gameObject.SetActive(false);
		}
	}

	public void EnableNavigationBtns()
	{
		if(previousEntry != null)
		{
			Debug.Log(1);
			PrevEntryButton.gameObject.SetActive(true);
		}

		if(nextEntry != null)
		{
			Debug.Log(2);
			NextEntryButton.gameObject.SetActive(false);
		}
	}

}
