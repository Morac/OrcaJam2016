using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollectionManager : Singleton<CollectionManager>
{
	public enum EntryID
	{
		Fish = 0,
		BlueFish = 1,
		RedFish = 2,
	}

	public const string BaseKey = "CollectionManager.";

	public static string FormatNameKey(EntryID id)
	{
		return BaseKey + id + ".HighScoreName";
	}

	public static string FormatDepthKey(EntryID id)
	{
		return BaseKey + id + ".HighScoreDepth";
	}

	[System.Serializable]
	public class Entry
	{
		public EntryID ID;
		public string Name;
		[TextArea]
		public string Description;
		public Sprite Image;

		[System.NonSerialized]
		public string HighScoreName;

		[System.NonSerialized]
		public float HighScoreDepth;

		public bool HasHighScore { get { return !string.IsNullOrEmpty(HighScoreName); } }

		public void SaveToPrefs()
		{
			PlayerPrefs.SetString(FormatNameKey(ID), HighScoreName);
			PlayerPrefs.SetFloat(FormatDepthKey(ID), HighScoreDepth);
		}

		public void LoadFromPrefs()
		{
			HighScoreName = PlayerPrefs.GetString(FormatNameKey(ID), "");
			HighScoreDepth = PlayerPrefs.GetFloat(FormatDepthKey(ID), 0f);
		}
	}

	public List<Entry> CollectionEntries = new List<Entry>();

	protected override void Awake()
	{
		foreach (var entry in CollectionEntries)
		{
			entry.LoadFromPrefs();
		}

		base.Awake();
	}

	public bool IsHighScore(EntryID id, float depth)
	{
		var entry = CollectionEntries.First(item => item.ID == id);

		return entry.HighScoreDepth < depth;
	}

	public void RegisterScore(EntryID id, string name, float depth)
	{
		var entry = CollectionEntries.First(item => item.ID == id);

		if (entry.HighScoreDepth < depth)
		{
			entry.HighScoreDepth = depth;
			entry.HighScoreName = name;
			entry.SaveToPrefs();
		}
	}
}
