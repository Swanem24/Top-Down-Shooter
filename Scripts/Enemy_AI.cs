using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI : MonoBehaviour
{
	public Transform target;
	
	public float speed = 200f;
	public float nextWaypointDistance = 3f;
	
	Path path;
	int currentWaypoint = 0;
	bool reachedEndOfPath = false;
	
	Seeker seeker;
	Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		target = GameObject.FindWithTag ("Player").transform;
		
		InvokeRepeating("UpdatePath", 0f, .5f);

    }
	
	void UpdatePath()
	{
		if (seeker.IsDone())
			seeker.StartPath(rb.position, target.position, OnPathComplete);
	}
	
	void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		
		Vector3 targetdif = transform.position - target.position;
		
		transform.Rotate(0.0f, 0.0f, targetdif.z);
	
        if (path == null)
			return;
		
		if (currentWaypoint >= path.vectorPath.Count)
		{
			reachedEndOfPath = true;
			return;
		}
		else
		{
			reachedEndOfPath = false;
		}
		
		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * speed * Time.deltaTime;
		
		rb.AddForce(force);
		
		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
		
		if (distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}
    }
}
