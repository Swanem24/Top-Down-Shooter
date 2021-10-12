using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject explosion_prefab;

	void OnCollisionEnter2D(Collision2D collision)
    {
		GameObject effect = Instantiate(explosion_prefab, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}
}
