using UnityEngine;
using DG.Tweening;
using InControl;

public class TitleSceneManager : MonoBehaviour
{
	public AudioSource Ambience;
	
	//class TitleActions : PlayerActionSet
	//{
	//	public PlayerAction Up;
	//	public PlayerAction Down;
	//	public PlayerOneAxisAction ChangeSelection;
	//	public PlayerAction Select;

	//	public TitleActions()
	//	{
	//		Up = CreatePlayerAction("Up");
	//		Down = CreatePlayerAction("Down");
	//		ChangeSelection = CreateOneAxisPlayerAction(Down, Up);
	//		Select = CreatePlayerAction("Select");

	//		Up.AddDefaultBinding(Key.W);
	//		Up.AddDefaultBinding(Key.UpArrow);
	//		Up.AddDefaultBinding(InputControlType.LeftStickUp);
	//		Up.AddDefaultBinding(InputControlType.RightStickUp);
	//		Down.AddDefaultBinding(Key.D);
	//		Down.AddDefaultBinding(Key.DownArrow);
	//		Down.AddDefaultBinding(InputControlType.LeftStickDown);
	//		Down.AddDefaultBinding(InputControlType.RightStickDown);

	//		Select.AddDefaultBinding(Key.Space);
	//		Select.AddDefaultBinding(Key.Return);
	//		Select.AddDefaultBinding(InputControlType.Action1);
	//	}
	//}

	//TitleActions actions = new TitleActions();

	void Start()
	{
		Ambience.volume = 0;
		Ambience.DOFade(1, 2);
	}
}
