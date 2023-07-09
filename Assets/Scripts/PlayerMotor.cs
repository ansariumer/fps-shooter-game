using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool isGrounded;
	private float Speed = 5f;

	public float gravity = -9.8f;
	public float jumpHeight = 3f;
	public float walkSpeed;
	public float sprintSpeed;
	public float crouchingSpeed;
	public float crouchTimer;


	Rigidbody rb;

	[Header("Crouching")]
	public float crouchSpeed;
	public float crouchYScale;
	private float startYScale;

	[Header("Keybinds")]
	public KeyCode sprintKey = KeyCode.LeftShift;
	public KeyCode crouchKey = KeyCode.LeftControl;
    
	public MovementState state;
	public enum MovementState{
		walking,
		crouching,
		sprinting,
		air,
	}

	private void StateHandler(){

		// mode - crouching
		if (Input.GetKey(crouchKey)){
			state = MovementState.crouching;
			Speed = crouchingSpeed;
		}
		// mode - Sprinting
		if(isGrounded && Input.GetKey(sprintKey)){
			state = MovementState.sprinting;
			Speed = sprintSpeed;
		}

		// mode - walking 
		else if (isGrounded){
			state = MovementState.walking;
			Speed = walkSpeed;
		}

		// Mode - air
		else{
			state = MovementState.air;
		}
	}

	private void crouching(){
		// // start crouching
		// if (Input.GetKeyDown(crouchKey)){
		// 	transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
		// 	rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
		// }

		// // stop crouching
		// if (Input.GetKeyUp(crouchKey)){
		// 	transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
		// }
	}

	// private void start(){
	// 	startYScale = transform.localScale.y; 
	// }
	// public bool sprinting = false;
	// public bool lerpCrouch = false;
	// public bool crouching = false;
	// public float crouchTimer = 3;
	
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
		startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
		isGrounded = controller.isGrounded; 
		StateHandler();
		// start crouching
		if (Input.GetKeyDown(crouchKey)){
			transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
			rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
		}

		// stop crouching
		if (Input.GetKeyUp(crouchKey)){
			transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
		}
		// if (lerpCrouch){
		// 	crouchTimer += Time.deltaTime;
		// 	float p = crouchTimer / 1;
		// 	p *= p;
		// 	if (crouching){
		// 	    controller.height = Mathf.Lerp(controller.height, 1, p);
		// 	}
		//     else{
		// 		controller.height = Mathf.Lerp(controller.height, 2, p);
		// 	}
		// 	if (p > 1){
		// 		lerpCrouch = false;
		// 		crouchTimer = 3f;
		// 	}	
		}  
    // }
	// public void Crouch()
	// {
	//     crouching = !crouching;
	//     crouchTimer = 3;
	// 	lerpCrouch = false;
    // }
	// public void Sprit()
	// {
	// 	sprinting = !sprinting;
	// 	if (sprinting)
	// 	    Speed = 15;
	// 	else
	// 	    Speed = 5;
	// }

    // receive the inputs for our InputManager.cs and apply them to our caracter copntroller
	public void ProcessMove(Vector2 input)
	{
		Vector3 moveDirection = Vector3.zero;
		moveDirection.x = input.x;
		moveDirection.z = input.y;
		controller.Move(transform.TransformDirection(moveDirection) * Speed * Time.deltaTime);
		playerVelocity.y += gravity * Time.deltaTime;
		if (isGrounded && playerVelocity.y < 0)
		playerVelocity.y = -2f;
		controller.Move(playerVelocity * Time.deltaTime);
		Debug.Log(playerVelocity.y);
	}
	public void Jump()
	{
		if (isGrounded)
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
		}
	}
}