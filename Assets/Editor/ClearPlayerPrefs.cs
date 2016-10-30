using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs
{
	[MenuItem("Fancy/Clear player prefs")]
	static void Clear()
	{
		PlayerPrefs.DeleteAll();
	}
}
