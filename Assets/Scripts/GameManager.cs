using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
	public FancyPlayerMover Player;

	public List<Obstacle> Obstacles = new List<Obstacle>();
	public float ObstacleDensity = 1;

	public List<Target> Targets = new List<Target>();
	public float TargetDensity = 5;

	public float UpdateRange = 10;
	public float MaxY = 0;
	


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
			else
			{
				UpdateGameWorld();
			}
		}
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

	//spawn nearby items if needed
	void UpdateGameWorld()
	{
		
	}
}
