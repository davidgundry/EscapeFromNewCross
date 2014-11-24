using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public float speed;
	public float jumpSpeed;
	public float gravity;
	public float yVelocity;
	public float rotation;
	public float posX;
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
	  else
	  {
	      yVelocity -= gravity;
	  }
	  float moveHorizontal = Input.GetAxis("Horizontal");
	  float moveVertical = Input.GetAxis("Vertical");
	  Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
	  movement = transform.TransformDirection(movement);
	  cController.Move(movement * speed *  Time.deltaTime);
	  cController.Move(new Vector3(0.0f,yVelocity,0.0f) * Time.deltaTime);
	  
	  posX = Input.GetAxis("Mouse X");
	  Vector3 rot = transform.localEulerAngles;
	  rotation += posX * turnSpeed;
	  rot.y = rotation;
	  transform.localEulerAngles = rot;
	}
	
	
	
}
