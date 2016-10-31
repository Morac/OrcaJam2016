using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioSplash : MonoBehaviour
{
	public List<AudioClip> clips = new List<AudioClip>();

	AudioSource theAudio;
	bool played = false;

	void Start()
	{
		theAudio = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		if(!played && transform.position.y < 0)
		{
			played = true;
			theAudio.PlayOneShot(clips[Random.Range(0, clips.Count)]);
		}
	}
}
