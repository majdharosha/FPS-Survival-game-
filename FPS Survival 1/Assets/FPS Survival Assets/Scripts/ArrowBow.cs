using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBow : MonoBehaviour {


	Rigidbody mybody;

	private float speed = 30f;
	private float Deactivate_Time = 3f;

	private float damage = 35f;

	void Awake()
    {
		mybody = GetComponent<Rigidbody>();
    }


	void Start () {
		Invoke("DeactivateGameObject", Deactivate_Time);
	}
	

	void Update () {
		
	}


	public void Launch (Camera mainCamera )
    {
		mybody.velocity = mainCamera.transform.forward * speed;
		transform.LookAt(transform.position + mybody.velocity);

    }

	void DeactivateGameObject()
    {
		if (gameObject.activeInHierarchy)
        {
			gameObject.SetActive(false);
        }
    }


	void OnTriggerEnter (Collider target)
    {
		if (target.gameObject.tag == (Tags.ENEMY_TAG))
        {
			target.GetComponent<Health>().ApplyDamage(damage);
			gameObject.SetActive(false);
        }
    }
}
