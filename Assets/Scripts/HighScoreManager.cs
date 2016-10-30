using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreManager : Singleton<HighScoreManager>
{
	Text theText;

	const string ScoreKey = "HighScore";

	void Start()
	{
		theText = GetComponent<Text>();
		DisplayText();
	}

	public bool RegisterScore(float newScore)
	{
		if (PlayerPrefs.GetFloat(ScoreKey, 0) > newScore)
			return false;

		PlayerPrefs.SetFloat(ScoreKey, newScore);
		DisplayText();
		return true;
	}

	void DisplayText()
	{
		var score = PlayerPrefs.GetFloat(ScoreKey, 0);
		theText.text = (score * 10).ToString("0.0") + " cm";
	}
}
