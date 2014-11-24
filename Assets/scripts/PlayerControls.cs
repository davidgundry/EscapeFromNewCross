using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public float speed;
	public float jumpSpeed;
	public float gravity;
	private float yVelocity;
	private float rotation;
	private float posX;
	public float turnSpeed;
	
	private CharacterController cController;
	
	// Use this for initialization
	void Start () {
	  cController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate ()
	{
	  
	  if (cController.isGrounded)
	  {
	      yVelocity = 0;
	      if (Input.GetButtonDown("Jump"))
		  yVelocity = jumpSpeed;
	  }
	  yVelocity -= gravity;
	  
	  float moveHorizontal = Input.GetAxis("Horizontal");
	  float moveVertical = Input.GetAxis("Vertical");
	  Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical) * speed;
	  movement = transform.TransformDirection(movement);
	  movement.y += yVelocity;
	  cController.Move(movement * Time.deltaTime);
	  
	  posX = Input.GetAxis("Mouse X");
	  Vector3 rot = transform.localEulerAngles;
	  rotation += posX * turnSpeed;
	  rot.y = rotation;
	  transform.localEulerAngles = rot;
	}
	
	
	
}
