using UnityEngine;

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
			if(Player.transform.position.y > 0 && Player.caughtTarget != null)
			{
				Debug.Log("End game!!!!");
			}
		}
	}
}
