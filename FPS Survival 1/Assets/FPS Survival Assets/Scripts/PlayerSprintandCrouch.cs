using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintandCrouch : MonoBehaviour {

	private PlayerMovement playermovement;
	private CharacterController characterController;
	public float Sprint_Speed = 10f;
	public float Move_Speed = 5f;
	public float Crouch_Speed = 2f;

	private Transform Look_Root;
	private float stand_Height = 1.6f;
	private float crouch_Height = 1f;

	private bool IsCrouching;

	private PlayerFootSteps playersteps;

	private float sprint_Volume = 1f;
	private float crouch_Volume = 0.1f;
	private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f; 

	private float walk_Step_Distance = 0.4f;
	private float sprint_Step_Distance = 0.25f;
	private float crouch_Step_Distance = 0.5f;


	private PlayerStats playerstats;

	private float sprintvalue = 100;
	private float sprintThreshold = 10f; 


	void Awake  () {
		playermovement = GetComponent<PlayerMovement>();

		Look_Root = transform.GetChild(0);

		playersteps = GetComponentInChildren<PlayerFootSteps>();

		playerstats = GetComponent<PlayerStats>();

		characterController = GetComponent<CharacterController>();
	}

	void Start()
    {
		playersteps.volume_min = walk_Volume_Min;
		playersteps.volume_max = walk_Volume_Max;
		playersteps.step_Distance = walk_Step_Distance;

	}
	
	void Update () {
		Sprint();
		Crouch();

	}

	void Sprint()
    {



		if (sprintvalue > 0)
        {
			if (Input.GetKeyDown(KeyCode.LeftShift) && !IsCrouching)
			{
				playermovement.speed = Sprint_Speed;
				playersteps.step_Distance = sprint_Step_Distance;
				playersteps.volume_min = sprint_Volume;
				playersteps.volume_max = sprint_Volume;


			}
		}
		

		if (Input.GetKeyUp(KeyCode.LeftShift) && !IsCrouching)
		{
			playermovement.speed = Move_Speed;
			playersteps.step_Distance = walk_Step_Distance;
			playersteps.volume_min = walk_Volume_Min;
			playersteps.volume_max = walk_Volume_Max;

		}

		if (Input.GetKey(KeyCode.LeftShift) && !IsCrouching &&
			characterController.velocity.sqrMagnitude > 0)
		{
			sprintvalue -= sprintThreshold * Time.deltaTime;

			if (sprintvalue <= 0f)
            {
				sprintvalue = 0f; 
				playermovement.speed = Move_Speed;
				playersteps.step_Distance = walk_Step_Distance;
				playersteps.volume_min = walk_Volume_Min;
				playersteps.volume_max = walk_Volume_Max;
			}

			playerstats.Stamina_Stats(sprintvalue);
		}
		else
        {
			if (sprintvalue != 100)
            {
				sprintvalue += (sprintThreshold / 2) * Time.deltaTime;
				playerstats.Stamina_Stats(sprintvalue);

				if (sprintvalue > 100f)
                {
					sprintvalue = 100f;

                }
			}
        }




	}


	void Crouch()
    {
		if (Input.GetKeyDown(KeyCode.C))
        {
			if (IsCrouching)
            {
				Look_Root.localPosition = new Vector3(0, stand_Height, 0);
				playermovement.speed = Move_Speed;
				IsCrouching = false;

				playersteps.step_Distance = walk_Step_Distance;
				playersteps.volume_min = walk_Volume_Min;
				playersteps.volume_max = walk_Volume_Max;
			}
			else
            {
				Look_Root.localPosition = new Vector3(0, crouch_Height, 0);
				playermovement.speed = Crouch_Speed;
				IsCrouching = true;

				playersteps.step_Distance = crouch_Step_Distance;
				playersteps.volume_min = crouch_Volume;
				playersteps.volume_max = crouch_Volume;
			}
        }
    }
}
