using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollectionUIController : MonoBehaviour
{
	public CollectionUIEntry EntryPrefab;

	void Start()
	{
		var entries = CollectionManager.Instance.CollectionEntries;
		List<CollectionUIEntry> entryInstances = new List<CollectionUIEntry>();

		for (int i = 0; i < entries.Count; i++)
		{
			var inst = Instantiate(EntryPrefab);
			inst.transform.SetParent(transform, false);
			inst.gameObject.SetActive(true);
			entryInstances.Add(inst);
		}

		for (int i = 0; i < entryInstances.Count; i++)
		{
			var prev = i > 0 ? entryInstances[i - 1] : null;
			var next = i < entryInstances.Count - 1 ? entryInstances[i + 1] : null;
			entryInstances[i].Setup(entries[i], prev, next);
		}

		entryInstances[0].EnableNavigationBtns();

		EntryPrefab.gameObject.SetActive(false);

		var layoutElem = EntryPrefab.GetComponent<LayoutElement>();
		var layoutGroup = GetComponent<HorizontalLayoutGroup>();
		transform.position = new Vector3(layoutElem.preferredWidth + layoutGroup.spacing, transform.position.y, transform.position.z);
	}
}
