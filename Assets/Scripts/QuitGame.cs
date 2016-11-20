using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitGame : MonoBehaviour
{
	void Start()
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			gameObject.SetActive(false);
		}
		else
		{
			var btn = GetComponent<Button>();
			btn.onClick.AddListener(OnClick);
		}
	}

	void OnClick()
	{
		Application.Quit();
	}
}
