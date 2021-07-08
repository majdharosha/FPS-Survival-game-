using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

	public float damage = 2f;
	public float radius = 1f;
	public LayerMask layermask; 



	
	void Update () {

		Collider[] hits = Physics.OverlapSphere(transform.position, radius, layermask);

		if (hits.Length > 0)
        {
			gameObject.SetActive(false);

			hits[0].GetComponent<Health>().ApplyDamage(damage);
        }

	}
}
