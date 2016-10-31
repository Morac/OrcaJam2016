using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class LoadSceneButton : MonoBehaviour
{
	public int SceneID = 1;
	public Image Fade;
	public AudioSource ambience;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		ambience.DOFade(0, 1);
		Fade.DOFade(1, 2).OnComplete(() => SceneManager.LoadScene(SceneID));
	}
}
