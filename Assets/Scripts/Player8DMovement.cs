using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player8DMovement : MonoBehaviour {

	Rigidbody rb;
	[Header("Movement Settings")]
	public float fSpeedForce = 50f;
	public float fMaxSpeed = 5f;
	public float jumpForce = 400;


	bool canJump = true;
	public bool isJumping = false;

	Animator anim;

	Vector3 v3MoveDirection;

	void Awake()
	{
		rb = GetComponent<Rigidbody> ();
	}
	void Start()
	{
		anim = GetComponent<Animator> ();
	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Space) && canJump == true)
		{
			isJumping = true;
			canJump = false;
		}

	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		v3MoveDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		if(moveVertical == 0 && moveHorizontal == 0)
		{
			rb.velocity = Vector3.zero;
		}

		rb.AddForce (v3MoveDirection * fSpeedForce);
		if (rb.velocity.magnitude > fMaxSpeed)
			rb.velocity = v3MoveDirection.normalized * fMaxSpeed;

		if(Mathf.Abs(rb.velocity.magnitude) > 0)
		{
			//anim.SetBool ("isWalking", true);
			transform.rotation = Quaternion.LookRotation (v3MoveDirection);
		}
		if(rb.velocity.magnitude == 0)
		{			
			//anim.SetBool ("isWalking", false);	
		}



		if(isJumping == true)
		{			
			//rb.AddForce (new Vector3 (0, jumpForce, 0));
			rb.AddForce (transform.up * jumpForce);
			isJumping = false;
		}


	}

	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Ground")
		{
			canJump = true;
		}
	}
}