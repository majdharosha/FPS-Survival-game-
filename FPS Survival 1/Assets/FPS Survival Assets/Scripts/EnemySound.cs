using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour {


	AudioSource audiosource;
	[SerializeField]
	private AudioClip scream_clip, die_clip;
	[SerializeField]
	private AudioClip[] attack_clips; 


	void Awake()
    {
		audiosource = GetComponent<AudioSource>();

    }


	public void PlayScreamSound()
    {
		audiosource.clip = scream_clip;
		audiosource.Play();
    }

	public void PlayAttackSound()
	{
		audiosource.clip = attack_clips[Random.Range(0,attack_clips.Length)];
		audiosource.Play();
	}

	public void PlayDeadSound()
	{
		audiosource.clip = die_clip;
		audiosource.Play();
	}
}
