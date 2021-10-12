using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
	public float speed;
	
	private Transform target;
	
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	void Update()
	{									//			From				to				at what speed
		transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}
	

	void OnTriggerEnter2D(Collider2D col)
    {
		if(col.tag == "Player")
		Destroy(gameObject);
	}
}
