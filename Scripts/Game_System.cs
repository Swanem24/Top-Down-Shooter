using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//	All the states in the game	//
public enum GameState {START, START_WAVE, END_WAVE, BREAK, WON, LOST}

public class Game_System : MonoBehaviour
{
	//	Game UI	//
	public GameState state;
	public Text dialogue;
	int wave = 1;
	
	//	Player Prefab	//
	//public GameObject player_prefab;
	
	//	Count Down Clock	//
	float timeLeft = 30.0f;
	bool countdown = false;
	
	//	Enemy	//
	public GameObject enemy_prefab;
	int num_of_enemies = 10;
	bool check_enemy = false;
	

    // Start is called before the first frame update
    void Start()
    {
		state = GameState.START;
		StartCoroutine(SetupBattle());
		dialogue.text = "Loading Wave";
		//Instantiate(player_prefab, new Vector3(0,0,0), Quaternion.identity);
    }
	
	void Update()
	{	
		//	Checking if any enemies are left	//
	
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		if(check_enemy == true)
		{
			if (enemies.Length == 0)
			{
			    state = GameState.END_WAVE;
				check_enemy = false;
				StartCoroutine(End_Wave());
			}
		}
		else
		{
			check_enemy = false;
		}
		
		
		//	Countdown clock	//
		if(countdown == true)
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0)
			{
				StartCoroutine(SetupBattle());
				countdown = false;
			}
			else
			{
				dialogue.text = "Next wave in " + Mathf.Round(timeLeft) + " sec";
				timeLeft -= Time.deltaTime;
			}
		}
		else
		{
			countdown = false;
		}
	}
	
	//	End Turn	//
	IEnumerator End_Wave()
	{
		dialogue.text = "This wave has ended";
		yield return new WaitForSeconds(2f);
		StartCoroutine(Break_time());
	}

	//	Set up the Battle	//
	IEnumerator SetupBattle()
	{	
		yield return new WaitForSeconds(2f);
		
		dialogue.text = "WAVE " + wave;
		
		state = GameState.START_WAVE;
		SpawnEnemy();
	}
	
	
	//	Spawn the enemies	//
	void SpawnEnemy()
	{
		for (int i = num_of_enemies; i > 0; i--)
		{
			//	UP	//
			Instantiate(enemy_prefab, new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(40.0f, 50.0f), 0), Quaternion.identity);
			
			//	RIGHT	//
			Instantiate(enemy_prefab, new Vector3(Random.Range(39.0f, 45.0f), Random.Range(0.0f, 1.0f), 0), Quaternion.identity);
			
			//	LEFT	//
			Instantiate(enemy_prefab, new Vector3(Random.Range(-45.0f, -43.0f), Random.Range(-20.0f, 10.0f), 0), Quaternion.identity);
			
			//	DOWN	//
			Instantiate(enemy_prefab, new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(-35.0f, -37.0f), 0), Quaternion.identity);
		}
		
		check_enemy = true;
	}
	
	//	A short break after the wave eneded	//
	IEnumerator Break_time()
	{
		state = GameState.BREAK;
		timeLeft = 30.0f;
		countdown = true;

		num_of_enemies += 1;
		wave += 1;
		yield return new WaitForSeconds(20f);

		//StartCoroutine(SetupBattle());
	}
	
	
	
	
	
	
	
	
	//	Restart the game	//
	public void Restart_Game()
	{
		 SceneManager.LoadScene("Story");
	}
}
