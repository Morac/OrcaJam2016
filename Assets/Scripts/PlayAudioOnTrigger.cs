using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class PlayAudioOnTrigger : MonoBehaviour
{
	public List<AudioClip> clips = new List<AudioClip>();

	AudioSource theAudio;

	void Start()
	{
		theAudio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<FancyPlayerMover>() && !theAudio.isPlaying)
		{
			theAudio.PlayOneShot(clips[Random.Range(0, clips.Count)]);
		}
	}
}
