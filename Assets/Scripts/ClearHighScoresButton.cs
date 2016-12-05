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

		foreach (CollectionManager.EntryID id in System.Enum.GetValues(typeof(CollectionManager.EntryID)))
		{
			if (!PlayerPrefs.HasKey(CollectionManager.FormatNameKey(id)))
			{
				button.interactable = false;
				break;
			}
		}

	}

	void OnClick()
	{
		//TODO: Confirm dialogue
		PlayerPrefs.DeleteAll();
		button.interactable = false;
	}
}
