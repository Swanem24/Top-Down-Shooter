using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_thrower : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Enemy")
		{	
			Destroy(collision.collider.gameObject);
		}
	}
}
