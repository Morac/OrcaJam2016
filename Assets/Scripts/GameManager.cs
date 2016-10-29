using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
	public FancyPlayerMover Player;

	public List<GameObject> Obstacles = new List<GameObject>();
	public float ObstacleDensity = 1;

	public List<GameObject> Targets = new List<GameObject>();
	public float TargetDensity = 5;

	public float UpdateRange = 10;
	public float PositionVariance = 2;
	public float MaxY = 0;

	Dictionary<Vector2, GameObject> SpawnedObstacles = new Dictionary<Vector2, GameObject>();
	Dictionary<Vector2, GameObject> SpawnedTargets = new Dictionary<Vector2, GameObject>();

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

		UpdateSpawnedObjects(ObstacleDensity, Obstacles, SpawnedObstacles);
		UpdateSpawnedObjects(TargetDensity, Targets, SpawnedTargets);
	}

	void EndGame()
	{
		Player.enabled = false;

		Player.victoryParticles.Play();

		//high score??
		var seq = DOTween.Sequence();
		seq.AppendInterval(2);
		seq.AppendCallback(() => UIManager.Instance.DoFade());
		seq.AppendInterval(2);
		seq.AppendCallback(() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));
	}

	void UpdateSpawnedObjects(float density, List<GameObject> prefabs, Dictionary<Vector2, GameObject> spawnedDictionary)
	{
		var pos_x = Player.transform.position.x - Player.transform.position.x % density;
		var pos_y = Player.transform.position.y - Player.transform.position.y % density;

		for(float x = pos_x - UpdateRange; x < pos_x + UpdateRange; x += density)
		{
			for(float y = pos_y - UpdateRange; y < pos_y + UpdateRange && y < MaxY; y += density)
			{
				Vector2 pos = new Vector2(x, y);
				if(spawnedDictionary.ContainsKey(pos))
				{
					continue;
				}

				var inst = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
				inst.transform.position = pos + new Vector2(Random.Range(-PositionVariance, PositionVariance), Random.Range(-PositionVariance, PositionVariance));

				spawnedDictionary[pos] = inst;
			}
		}
	}
}
