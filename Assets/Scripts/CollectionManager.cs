using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollectionManager : Singleton<CollectionManager>
{
	public enum EntryID
	{
		Fish,
		RedFish,
		BlueFish
	}

	const string BaseKey = "CollectionManager.";

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
			PlayerPrefs.SetString(BaseKey + ID + ".HighScoreName", HighScoreName);
			PlayerPrefs.SetFloat(BaseKey + ID + ".HighScoreDepth", HighScoreDepth);
		}

		public void LoadFromPrefs()
		{
			HighScoreName = PlayerPrefs.GetString(BaseKey + ID + ".HighScoreName", "");
			HighScoreDepth = PlayerPrefs.GetFloat(BaseKey + ID + ".HighScoreDepth", 0f);
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
