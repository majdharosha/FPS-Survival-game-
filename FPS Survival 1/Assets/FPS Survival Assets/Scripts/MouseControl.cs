using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

	[SerializeField]
	private Transform playerRoot, lookRoot;

	[SerializeField]
	private bool invert;

	[SerializeField]
	private bool can_Unlock = true;

	[SerializeField]
	private float sensitivity = 5f;

	[SerializeField]
	private int smooth_steps = 10;

	[SerializeField]
	private float smooth_weight = 0.4f;

	[SerializeField]
	private float roll_Angle = 10f;

	[SerializeField]
	private float Roll_Speed = 3f; 


	[SerializeField]
	private Vector2 default_look_limit = new Vector2(-70f, 80f);

	private Vector2 look_angles;

	private Vector2 current_mouse_look;

	private Vector2 smoothMove;

	private float current_roll_angle;

	private int last_look_frame; 




	void Start () {
		Cursor.lockState = CursorLockMode.Locked; 

	}
	


	void Update () {
		LockandUnlockCursor();

		if (Cursor.lockState == CursorLockMode.Locked)
        {
			LookAround();

		}
	}

	void LockandUnlockCursor()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (Cursor.lockState == CursorLockMode.Locked)
            {
				Cursor.lockState = CursorLockMode.None; 

            }
			else
            {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false; 
			}
        }

    }

	void LookAround()
    {
		current_mouse_look = new Vector2(Input.GetAxis(MouseAxis.MouseY), Input.GetAxis(MouseAxis.MouseX));

		look_angles.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f);
		look_angles.y += current_mouse_look.y * sensitivity;

		look_angles.x = Mathf.Clamp(look_angles.x, default_look_limit.x, default_look_limit.y);

		current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw(MouseAxis.MouseX) * roll_Angle,
			Time.deltaTime * Roll_Speed);

		lookRoot.localRotation = Quaternion.Euler(look_angles.x , 0f, current_roll_angle);
		playerRoot.localRotation = Quaternion.Euler(0f, look_angles.y, 0f);



		

	}

}
