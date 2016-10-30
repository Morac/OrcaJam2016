using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
	public FancyPlayerMover Player;

	public float UpdateRange = 10;

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

	public bool GameStarted { get; private set; }

	void Start()
	{
		Player.enabled = false;
	}

	void Update()
	{
		if(!GameStarted)
		{
			if(Player.actions.StartGame.WasPressed)
			{
				GameStarted = true;
				Player.enabled = true;
			}
		}
		else
		{
			if(Player.enabled && Player.transform.position.y > 0 && Player.caughtTarget != null)
			{
				//end game
				EndGame();
			}
		}
		
		foreach(var item in SpawnedObjects)
		{
			UpdateSpawnedObjects(item);
		}
	}

	void EndGame()
	{
		Player.enabled = false;

		Player.victoryParticles.Play();

		if(HighScoreManager.Instance.RegisterScore(Player.caughtTarget.Size))
		{
			Debug.Log("High score!");
		}

		//high score??
		var seq = DOTween.Sequence();
		seq.AppendInterval(2);
		seq.AppendCallback(() => UIManager.Instance.DoFade());
		seq.AppendInterval(2);
		seq.AppendCallback(() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));
	}

	void UpdateSpawnedObjects(SpawnConfig config)
	{
		var pos_x = Player.transform.position.x - Player.transform.position.x % config.Density;
		var pos_y = Player.transform.position.y - Player.transform.position.y % config.Density;

        float range = 0;
        while (range < UpdateRange)
            range += config.Density;

		for(float x = pos_x - range; x < pos_x + range; x += config.Density)
		{
			for(float y = pos_y - range; y < pos_y + range && y < config.MaxY; y += config.Density)
			{
				Vector2 pos = new Vector2(x, y);
				if(config.Instances.ContainsKey(pos))
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
