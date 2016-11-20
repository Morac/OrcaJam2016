using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	public enum GameState
	{
		NotStarted,
		Started,
		Quitting
	}

	[HideInInspector]
	public GameState State = GameState.NotStarted;

	public FancyPlayerMover Player;

	public float UpdateRange = 10;

	public AudioSource Ambience;
	public AudioSource Music;
	public AudioSource ButtonClick;

	[System.Serializable]
	public class SpawnConfig
	{
		public List<GameObject> Prefabs = new List<GameObject>();
		public float Density;
		public float PositionVariance;
		public Dictionary<Vector2, GameObject> Instances = new Dictionary<Vector2, GameObject>();

		public float StartThreshold = 0.2f;
		public float ThresholdModifier = 0.01f;
		public float ScaleMin = 1;
		public float ScaleMax = 1;
		public float MaxY = 0;
	}

	public List<SpawnConfig> SpawnedObjects = new List<SpawnConfig>();
	bool ambience = true;

	void Start()
	{
		Player.enabled = false;
		Ambience.volume = 0;
		Ambience.DOFade(1, 1);
	}

	void Update()
	{
		switch (State)
		{
			default:
				break;
			case GameState.NotStarted:
				if (Player.actions.StartGame.WasPressed)
				{
					State = GameState.Started;
					Player.enabled = true;
				}
				else if (Player.actions.Escape.WasPressed)
				{
					//if escape pressed, go back to title scene
					State = GameState.NotStarted;
					UIManager.Instance.DoFade(1f);
					ButtonClick.Play();
					var seq = DOTween.Sequence();
					seq.Append(Ambience.DOFade(0, 1f));
					seq.AppendCallback(() => SceneManager.LoadScene(0));
				}
				break;
			case GameState.Started:
				if (Player.enabled && Player.transform.position.y > 0.5f && Player.caughtTarget != null)
				{
					//end game
					EndGame();
				}
				break;
		}

		foreach (var item in SpawnedObjects)
		{
			UpdateSpawnedObjects(item);
		}

		if (Player.transform.position.y < 0 && ambience)
		{
			Ambience.DOFade(0, 1);
			Music.DOFade(1, 1);
			ambience = false;
		}
		else if (Player.transform.position.y >= 0 && !ambience)
		{
			Ambience.DOFade(1, 1);
			Music.DOFade(0, 1);
			ambience = true;
		}
	}

	void EndGame()
	{
		Player.enabled = false;

		//Player.victoryParticles.Play();

		HighScoreManager.Instance.RegisterScore(Player.caughtTarget.Depth, EndGameCleanup);
	}

	void EndGameCleanup()
	{
		//cleanup
		Boat.Instance.PlayEndAnimation();

		var seq = DOTween.Sequence();
		seq.AppendInterval(4);
		seq.AppendCallback(() => { UIManager.Instance.DoFade(); Ambience.DOFade(0, 1); });
		seq.AppendInterval(2);
		seq.AppendCallback(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
	}

	void UpdateSpawnedObjects(SpawnConfig config)
	{
		var pos_x = Player.transform.position.x - Player.transform.position.x % config.Density;
		var pos_y = Player.transform.position.y - Player.transform.position.y % config.Density;

		float range = 0;
		while (range < UpdateRange)
			range += config.Density;

		for (float x = pos_x - range; x < pos_x + range; x += config.Density)
		{
			for (float y = pos_y - range; y < pos_y + range && y < config.MaxY; y += config.Density)
			{
				Vector2 pos = new Vector2(x, y);
				if (config.Instances.ContainsKey(pos))
				{
					continue;
				}

				var threshold = config.StartThreshold + Mathf.Abs(pos_y) * config.ThresholdModifier;
				if (Random.Range(0f, 1f) > threshold)
				{
					config.Instances[pos] = null;
				}
				else
				{
					var inst = Instantiate(config.Prefabs[Random.Range(0, config.Prefabs.Count)]);
					inst.transform.position = pos + new Vector2(Random.Range(-config.PositionVariance, config.PositionVariance), Random.Range(-config.PositionVariance, config.PositionVariance));
					inst.transform.localScale *= Random.Range(config.ScaleMin, config.ScaleMax);
					config.Instances[pos] = inst;
				}
			}
		}
	}
}
