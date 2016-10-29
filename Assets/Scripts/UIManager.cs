using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
	public Image Fadeout;

	public void DoFade()
	{
		Fadeout.DOFade(1f, 2f).SetUpdate(true);
	}
}
