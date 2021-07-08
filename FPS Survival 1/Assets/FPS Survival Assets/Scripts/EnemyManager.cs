using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {


	public static EnemyManager instance;

	[SerializeField]
	private GameObject cannibalPrefab, boarPrefab;

	public Transform[] cannibal_Spawn_Points, boar_Spawn_Points;

	[SerializeField]
	private int cannibal_Enemy_Count, boar_Enemy_Count;

	private int inital_Cannibal_Count, inital_Boar_Count;

	public float Wait_Before_Spawn_Enemies_Time = 10f;


	void Awake()
    {
		Makeinstance();
    }

	void Makeinstance()
    {
		if (instance == null)
        {
			instance = this;
        }
    }


	void Start () {
		inital_Boar_Count = boar_Enemy_Count;
		inital_Cannibal_Count = cannibal_Enemy_Count;

		SpawnEnemies();
		StartCoroutine("CheckToSpawnEnemies");

	}

	void SpawnEnemies()
    {
		SpawnCannibal();

		SpawnBoars();
	}

	void SpawnCannibal()
    {
		int index = 0; 


		for (int i = 0; i < cannibal_Enemy_Count; i++)
        {
			if (index >= cannibal_Spawn_Points.Length)
            {
				index = 0;
            }

			Instantiate(cannibalPrefab, cannibal_Spawn_Points[index].position, Quaternion.identity);
			index++;

        }
		cannibal_Enemy_Count = 0;


		


	}




	void SpawnBoars ()
	{

		int index = 0;

		for (int i = 0; i < boar_Enemy_Count; i++)
		{
			if (index >= boar_Spawn_Points.Length)
			{
				index = 0;
			}

			Instantiate(boarPrefab, boar_Spawn_Points[index].position, Quaternion.identity);
			index++;

		}
		boar_Enemy_Count = 0;
	}


	IEnumerator CheckToSpawnEnemies()
    {
		yield return new WaitForSeconds(Wait_Before_Spawn_Enemies_Time);

		SpawnCannibal();
		SpawnBoars();

		StartCoroutine("CheckToSpawnEnemies");
	}

	public void EnemyDied(bool cannibal)
    {
		if (cannibal)
        {
			cannibal_Enemy_Count++;
			if (cannibal_Enemy_Count > inital_Cannibal_Count)
            {
				cannibal_Enemy_Count = inital_Cannibal_Count;

			}
        }
		else
        {
			boar_Enemy_Count++;
			if (boar_Enemy_Count > inital_Boar_Count)
			{
				boar_Enemy_Count = inital_Boar_Count;

			}
		}


    }


	public void StopSpawning()
    {
		StopCoroutine(CheckToSpawnEnemies());
    }


	void Update () {
		
	}














}
