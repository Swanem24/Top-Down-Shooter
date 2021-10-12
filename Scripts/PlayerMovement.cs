using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	
	public Rigidbody2D rb;
	public Camera cam;
	
	Vector2 movement;
	Vector2 mousePos;
	
	public GameObject blood_prefab;
	
	bool isBouncing = false;
	
	//	Animation	//
	private Animator anim;

	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		
		//anim.SetBool("isWalking", true);
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
	
	void FixedUpdate()
	{
		if(!isBouncing) rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
		
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	 {
	     if(collision.gameObject.tag == "Enemy")
	     {
	         float bounce = 100f; //amount of force to apply
	         rb.AddForce(collision.contacts[0].normal * bounce);
	         isBouncing = true;
	         Invoke("StopBounce", 0.3f);
			 Instantiate(blood_prefab, transform.position, Quaternion.identity);
	     }
	 }
	 void StopBounce()
	 {
	     isBouncing = false;
	 }
}
