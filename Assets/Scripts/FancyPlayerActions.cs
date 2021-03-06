﻿using UnityEngine;
using InControl;

public class FancyPlayerActions : PlayerActionSet
{
	public PlayerAction StartGame;
	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerOneAxisAction MoveLateral;

	public PlayerAction Escape;

	//open catalogue
	//close catalogue
	//scroll catalogue
	
	public FancyPlayerActions()
	{
		StartGame = CreatePlayerAction("Start Game");
		Left = CreatePlayerAction("Left");
		Right = CreatePlayerAction("Right");
		MoveLateral = CreateOneAxisPlayerAction(Left, Right);
		Escape = CreatePlayerAction("Escape");
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
		actions.Escape.AddDefaultBinding(Key.Escape);

        //controller bindings
        actions.StartGame.AddDefaultBinding(InputControlType.Action1);
        actions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        actions.Left.AddDefaultBinding(InputControlType.RightStickLeft);
        actions.Right.AddDefaultBinding(InputControlType.RightStickRight);
        actions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
		actions.Escape.AddDefaultBinding(InputControlType.Start);

		return actions;
	}
}
