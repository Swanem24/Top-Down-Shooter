using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
	public int playerMaxHealth;
	public int playerCurrentHealth;
	
	public GameObject game_over;
	public GameObject Grunt_noise;
	
	void Update()
	{
		//	Check if health is below zero	//
		if (playerCurrentHealth <= 0)
		{
			Debug.Log("Game Over");
			game_over.SetActive(true);
		}
	}
	
	//	Taking Damage	//
	void TakeDamage(int damage)
	{
		playerCurrentHealth -= damage;
	}
	
	//	Checking collision between the player and enemy	//
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
			playerCurrentHealth -= 10;
			Instantiate(Grunt_noise);
        }
    }
	
	public void buy_health()
	{
		playerCurrentHealth = 100;
	}
}
