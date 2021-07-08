using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Health : MonoBehaviour {


	EnemyAnimations Enemy_Anim;
	EnemyController enemy_controller;
	NavMeshAgent navmeshagent;
	public float health = 100f;

	public bool is_player, is_boar, is_cannibal;

	private bool is_Dead;

	private EnemySound enemyaudio;

	private PlayerStats playerstats; 
	void Awake()
    {
		if (is_boar || is_cannibal)
		{
			Enemy_Anim = GetComponent<EnemyAnimations>();
			enemy_controller = GetComponent<EnemyController>();
			navmeshagent = GetComponent<NavMeshAgent>();
			enemyaudio = GetComponent<EnemySound>();
		}

		if (is_player)
        {
			playerstats = GetComponent<PlayerStats>();
        }



	}


	public void ApplyDamage(float damage)
    {
		if (is_Dead)
			return;

		health -= damage;

		if (is_player)
		{
			playerstats.Health_Stats(health);
		}

		if (is_boar || is_cannibal)
		{
			if (enemy_controller.enemy_state == EnemyState.PATROL)
            {
				enemy_controller.chase_Distance = 50f;
            }

		}


		if (health <= 0)
        {
			PlayerDied();
			is_Dead = true;
        }

	}


	void PlayerDied()
    {
		if (is_cannibal)
        {
			GetComponent<BoxCollider>().isTrigger = false;
			GetComponent<Animator>().enabled = false;
			GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);
			
			

			enemy_controller.enabled = false;
			navmeshagent.enabled = false;
			Enemy_Anim.enabled = false;
			StartCoroutine(DeadSound());
			EnemyManager.instance.EnemyDied(true);
		}


		if (is_boar)
        {
			navmeshagent.velocity = Vector3.zero;
			navmeshagent.isStopped = true;
			enemy_controller.enabled = false;

			StartCoroutine(DeadSound());
			Enemy_Anim.Dead();
			EnemyManager.instance.EnemyDied(false);
		}


		if (is_player)
        {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

			for (int i = 0; i < enemies.Length; i++)
            {
				enemies[i].GetComponent<EnemyController>().enabled = false;
            }

			GetComponent<PlayerMovement>().enabled = false;
			GetComponent<PLayerAttack>().enabled = false;
			GetComponent<WeaponsManager>().GetCurrentSelectedWeaponInfo().gameObject.SetActive(false);
			EnemyManager.instance.StopSpawning();
		}

		if (tag == Tags.PLAYER_TAG)
        {
			Invoke("RestartGame", 3f);
        }
		else
        {
			Invoke("TurnOffGameObject", 3f);
		}
    }


	void RestartGame()
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

    }

	void TurnOffGameObject()
    {
		gameObject.SetActive(false);
    }


	IEnumerator DeadSound()
    {
		yield return new WaitForSeconds(0.3f);
		enemyaudio.PlayDeadSound();
    }





















}
