using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }

	public static bool Exists()
	{
		return Instance != null;
	}
	
	protected virtual void Awake()
	{
		Debug.Assert(this is T);
		Debug.Assert(Instance == null);
		Instance = this as T;
	}
}
