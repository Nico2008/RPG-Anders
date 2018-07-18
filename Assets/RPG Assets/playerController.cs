using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	CharacterController cc;
	Animator anim;
	public float moveSpeed = 4f;
	public float jumpHight = 20f;
	float gravity = 0f;
	float jumpVelocity = 0;
	string state = "movement";



	// Use this for initialization

	void Start () {
		cc = GetComponent<CharacterController>();
		anim = GetComponent<Animator> ();	
	}

	//void swing()
	//{
		
	//	if (Input.GetMouseButtonDown(0))
	//	{
	//		ChangeState("Swing");

		//}
	//}

// Update is called once per frame

	void Update () {
		print (state);
		if (state == "Movement") {
		Movement();
		//swing();
		}

		if (state == "Jump" && cc.isGrounded) 
		{
			ChangeState ("Movement");
		}

		if (state == "movement")
		{
			Movement ();
		}

		if (state == "Jump") {
			Jump();
			Movement ();
		}
	}
	void Jump()
	{
		if (jumpVelocity < 0) {

			return;

		}

		jumpVelocity -= 1.25f;
	}
	void Movement() {
		if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded) {
				jumpVelocity = jumpHight; 
			print (jumpVelocity);
			ChangeState("Jump");
			//anim.SetTrigger ("Jump");
		} 	
		else {
		gravity += 0.25f;
		gravity = Mathf.Clamp(gravity, 1f, 20f);
			 
		}
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3 (x, 0, z).normalized;
		Vector3 velocity = direction * moveSpeed * Time.deltaTime;
		Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;
		Vector3 jumpVector = Vector3.up * jumpVelocity * Time.deltaTime;

		float percentSpeed = velocity.magnitude / (moveSpeed * Time.deltaTime);
		anim.SetFloat("movePercent", percentSpeed);
		
			cc.Move (jumpVector);
		if (velocity.magnitude > 0) {
			float yAngle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
			transform.localEulerAngles = new Vector3 (0, yAngle, 0);
		}

		cc.Move (velocity + gravityVector);

		if (cc.isGrounded) {
			gravity = 0;
		}
	}
	void ChangeState(string stateName)
	{

		state = stateName;
		anim.SetTrigger (stateName);
	}
}
