using UnityEngine;
using InControl;

public class FancyPlayerActions : PlayerActionSet
{
	public PlayerAction StartGame;
	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerOneAxisAction MoveLateral;

	//open catalogue
	//close catalogue
	//scroll catalogue
	
	public FancyPlayerActions()
	{
		StartGame = CreatePlayerAction("Start Game");
		Left = CreatePlayerAction("Left");
		Right = CreatePlayerAction("Right");
		MoveLateral = CreateOneAxisPlayerAction(Left, Right);
	}

	public static FancyPlayerActions Create()
	{
		var actions = new FancyPlayerActions();

		//keyboard bindings
		actions.StartGame.AddDefaultBinding(Key.Space);
		actions.Left.AddDefaultBinding(Key.A);
		actions.Left.AddDefaultBinding(Key.LeftArrow);
		actions.Right.AddDefaultBinding(Key.D);
		actions.Right.AddDefaultBinding(Key.RightArrow);

		//controller bindings


		return actions;
	}
}
