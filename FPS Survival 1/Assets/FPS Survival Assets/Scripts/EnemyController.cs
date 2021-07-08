using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
	PATROL,
	CHASE,
	ATTACK
}




public class EnemyController : MonoBehaviour {

	private EnemyState enemystate;

	private EnemyAnimations Enemy_Anim;
	private NavMeshAgent agent;

	public float Walk_Speed = 0.5f;
	public float run_Speed = 4f;

	public float chase_Distance = 7f;
	private float current_Chase_Distance;
	public float attack_Distance = 1.8f;
	public float chase_After_Attack_Distance = 2f;

	public float Patrol_Radius_Min = 20f, Patrol_Radius_Max = 60f;
	public float Patrol_forthis_Time = 15f;
	private float patrol_Timer;

	public float Wait_Before_Attack = 2f;
	private float attack_Timer;

	private Transform target;

	public GameObject attack_Point;

	EnemySound enemysound; 
	void Awake()
    {
		Enemy_Anim = GetComponent<EnemyAnimations>();
		agent = GetComponent<NavMeshAgent>();

		target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
		enemysound = GetComponent<EnemySound>();

    }

	void Start () {

		enemystate = EnemyState.PATROL;

		patrol_Timer = Patrol_forthis_Time;

		attack_Timer = Wait_Before_Attack;

		current_Chase_Distance = chase_Distance;

	}
	




	void Update () {
		if (enemystate == EnemyState.PATROL)
        {
			Patrol();
        }

		if (enemystate == EnemyState.CHASE)
		{
			Chase();
		}

		if (enemystate == EnemyState.ATTACK)
		{
			Attack();
		}

	}

	void Patrol()
    {
		agent.isStopped = false;
		agent.speed = Walk_Speed;

		patrol_Timer += Time.deltaTime;

		if (patrol_Timer > Patrol_forthis_Time)
        {
			SetNewRandomDestination();

			patrol_Timer = 0f;
        }

		if(agent.velocity.sqrMagnitude > 0)
        {
			Enemy_Anim.Walk(true);
        }
        else
        {
			Enemy_Anim.Walk(false);
		}

		if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
			Enemy_Anim.Walk(false);
			enemystate = EnemyState.CHASE;

			
			enemysound.PlayScreamSound();
        }
    }

	void Chase()
    {
		agent.isStopped = false;
		agent.speed = run_Speed;

		agent.SetDestination(target.position);

		if (agent.velocity.sqrMagnitude > 0)
		{
			Enemy_Anim.Run(true);
		}
		else
		{
			Enemy_Anim.Run(false);
		}

		if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
			Enemy_Anim.Run(false);
			Enemy_Anim.Walk(false);
			enemystate = EnemyState.ATTACK;

			if (chase_Distance != current_Chase_Distance)
            {
				chase_Distance = current_Chase_Distance;
            }

		}
		else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
			Enemy_Anim.Run(false);

			enemystate = EnemyState.PATROL;

			patrol_Timer = Patrol_forthis_Time;

			if (chase_Distance != current_Chase_Distance)
			{
				chase_Distance = current_Chase_Distance;
			}

		}

	}

	void Attack()
    {
		agent.velocity = Vector3.zero;
		agent.isStopped = true;

		attack_Timer += Time.deltaTime;


		if (attack_Timer > Wait_Before_Attack)
        {
			Enemy_Anim.Attack();

			attack_Timer = 0;

			enemysound.PlayAttackSound();

        }

		if (Vector3.Distance(transform.position, target.position) >
			attack_Distance + chase_After_Attack_Distance)
        {
			enemystate = EnemyState.CHASE;

        }
    }


	void SetNewRandomDestination()
    {
		float rand_Radius = Random.Range(Patrol_Radius_Min, Patrol_Radius_Max);

		Vector3 randDir = Random.insideUnitSphere * rand_Radius;
		randDir += transform.position;

		NavMeshHit navhit ;

		NavMesh.SamplePosition(randDir, out navhit, rand_Radius, -1);

		agent.SetDestination(navhit.position);

    }

	void Turn_ON_AttackPoint()
	{
		attack_Point.SetActive(true);
	}
	void Turn_OFF_AttackPoint()
	{
		if (attack_Point.activeInHierarchy)
		{
			attack_Point.SetActive(false);
		}

	}



	public EnemyState enemy_state
    {
		get; set;
    }

}
