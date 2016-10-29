using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
	public Image Fadeout;

	void Start()
	{
		Fadeout.color = new Color(Fadeout.color.r, Fadeout.color.g, Fadeout.color.b, 1);
		Fadeout.DOFade(0f, 0.5f);
	}

	public void DoFade()
	{
		Fadeout.DOFade(1f, 2f).SetUpdate(true);
	}
}
