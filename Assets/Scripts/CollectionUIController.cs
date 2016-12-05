using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class CollectionUIController : MonoBehaviour
{
	public CollectionUIEntry EntryPrefab;

	public int Spacing = 700;

	List<CollectionUIEntry> entryInstances = new List<CollectionUIEntry>();

	void Awake()
	{
		var entries = CollectionManager.Instance.CollectionEntries;
		entries.OrderBy(item => item.ID);

		for (int i = 0; i < entries.Count; i++)
		{
			var inst = Instantiate(EntryPrefab);
			inst.transform.SetParent(transform, false);
			inst.gameObject.SetActive(true);
			entryInstances.Add(inst);

			inst.transform.localPosition = new Vector3(i * Spacing, 0, 0);
		}

		for (int i = 0; i < entryInstances.Count; i++)
		{
			var prev = i > 0 ? entryInstances[i - 1] : null;
			var next = i < entryInstances.Count - 1 ? entryInstances[i + 1] : null;
			entryInstances[i].Setup(entries[i], prev, next);
		}

		entryInstances[0].EnableNavigationBtns();

		EntryPrefab.gameObject.SetActive(false);
	}

	public void ShowEntry(CollectionManager.EntryID id)
	{
		transform.DOLocalMoveX(-(int)id * Spacing, 0.5f).SetEase(Ease.OutQuad);
		foreach (var item in entryInstances)
			item.DisableNavigationButtons();
		entryInstances[(int)id].EnableNavigationBtns();
	}
}
