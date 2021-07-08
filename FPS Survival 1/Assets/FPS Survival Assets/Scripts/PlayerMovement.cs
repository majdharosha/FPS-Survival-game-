using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	private CharacterController characterController;
	private Vector3 Move_Direction;

	public float speed = 5f;
	private float gravity = 20f;

	public float JumpForce = 10f;
	private float Vertical_Velocity; 

	void Awake()
    {
		characterController = GetComponent<CharacterController>();

    }



	void Update ()
	{
		MovePlayer();
	}


	void MovePlayer()
    {
		Move_Direction = new Vector3(Input.GetAxis(Axis.Horizontal), 0f, Input.GetAxis(Axis.Vertical));

		Move_Direction = transform.TransformDirection(Move_Direction);
		Move_Direction *= speed * Time.deltaTime;

		ApplyGravity();
		characterController.Move(Move_Direction);
		
    }


	void ApplyGravity()
    {
		if (characterController.isGrounded)
        {
			Vertical_Velocity -= gravity * Time.deltaTime;
			PlayerJump();

        }
		else
        {
			Vertical_Velocity -= gravity * Time.deltaTime;

		}

		Move_Direction.y = Vertical_Velocity * Time.deltaTime; 

	}

	void PlayerJump()
    {
		if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
			Vertical_Velocity = JumpForce; 
        }
    }
}
