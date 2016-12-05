using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(CollectionManager))]
public class CollectionManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		var manager = target as CollectionManager;
		foreach(CollectionManager.EntryID id in System.Enum.GetValues(typeof(CollectionManager.EntryID)))
		{
			if(manager.CollectionEntries.Any(item => item.ID == id) == false)
			{
				manager.CollectionEntries.Add(new CollectionManager.Entry() { ID = id });
			}

			if(manager.CollectionEntries.Count(item => item.ID == id) > 1)
			{
				var c = GUI.color;
				GUI.color = Color.red;
				EditorGUILayout.LabelField("Too many entries with ID: " + id);
				GUI.color = c;
			}
		}

		manager.CollectionEntries = manager.CollectionEntries.OrderBy(item => item.ID).ToList();
	}
}
