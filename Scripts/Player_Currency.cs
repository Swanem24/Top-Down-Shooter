using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Currency : MonoBehaviour
{
	int player_money = 0;
	public Text money_text;
	public Text dialogue;
	
	public Player_Health hp;
	int currentHP;
	public bool bought_ar = false;
	
	void Update()
	{
		currentHP = hp.playerCurrentHealth;
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "Money")
		{
			player_money += 100;
			money_text.text = "$ " + player_money;
        }
    }
	
	public void minus_money()
	{
		if (currentHP >= 100)
		{
			dialogue.text = "Full HP";
		}
		else if (player_money < 100)
		{
			dialogue.text = "Not enough money";
		}
		else
		{
			player_money -= 100;
			money_text.text = "$ " + player_money;
		}
	}
	
	public void buy_assault_rifle()
	{
		//	If player has enough money they can buy it	//
		if (player_money >= 500 && bought_ar == false)
		{
			player_money -= 500;
			money_text.text = "$ " + player_money;
			dialogue.text = "You bought assault rifle";
			bought_ar = true;
			
		}
		//	If not enough money	//
		else if (player_money < 500)
		{
			dialogue.text = "Not enough money";
		}
		//	If player already bought assault rifle	//
		else if (bought_ar == true)
		{
			dialogue.text = "You already bought the Assault Rifle";
		}
	}
}
