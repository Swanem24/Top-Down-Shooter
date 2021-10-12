using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
	public float destroy_time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		destroy_time -= Time.deltaTime;
	
		if (destroy_time <= 0)
		{
			Destroy(gameObject);
		}
    }
}
