using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject money_prefab;
	public Transform enemy_transform;
	
	public Animator anim;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
			Instantiate(money_prefab, enemy_transform.position, Quaternion.identity);
			Destroy(gameObject);
        }
		
		else if (collision.gameObject.tag == "Player")
        {
			anim.SetBool("isAttacking", true);        
		}
    }
}
