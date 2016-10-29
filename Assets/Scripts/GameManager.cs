using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
	public FancyPlayerMover Player;

	bool GameStarted = false;

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
				Player.enabled = false;

				Player.victoryParticles.Play();

				//high score??
				var seq = DOTween.Sequence();
				seq.AppendInterval(2);
				seq.AppendCallback(() => UIManager.Instance.DoFade());
				seq.AppendInterval(2);
				seq.AppendCallback(() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));
			}
		}
	}
}
