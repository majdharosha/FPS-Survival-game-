using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour {

	private AudioSource footsteps_Sound;

	[SerializeField]
	private AudioClip[] footstepClip;
	private CharacterController character_controller;

	[HideInInspector]
	public float volume_min, volume_max;

	private float accumulated_Distance;

	[HideInInspector]
	public float step_Distance; 

	void Awake()
    {
		footsteps_Sound = GetComponent<AudioSource>();

		character_controller = GetComponentInParent<CharacterController>();

    }

	void Start () {
		
	}
	
	void Update () {
		CheckToPlayFootStepSound();
	}

	void CheckToPlayFootStepSound()
    {
		if (!character_controller.isGrounded)
         return; 


        if (character_controller.velocity.sqrMagnitude > 0)
        {
			accumulated_Distance += Time.deltaTime; 

			if (accumulated_Distance > step_Distance)
            {
				footsteps_Sound.volume = Random.Range(volume_min, volume_max);
				footsteps_Sound.clip = footstepClip[Random.Range(0, footstepClip.Length)];
				footsteps_Sound.Play();

				accumulated_Distance = 0f; 
		      
            }
			
        }
		else
		{
			accumulated_Distance = 0f;
		}


	}
}
