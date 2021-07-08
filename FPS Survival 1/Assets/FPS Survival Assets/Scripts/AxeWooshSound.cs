using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWooshSound : MonoBehaviour {


	[SerializeField]
	private AudioSource audiosource;

	[SerializeField]
	private AudioClip[] woosh_sounds;

	void PlayWooshSounds()
    {
		audiosource.clip = woosh_sounds[Random.Range(0, woosh_sounds.Length)];
		audiosource.Play();
    }
	


	void Update () {
		
	}



}
