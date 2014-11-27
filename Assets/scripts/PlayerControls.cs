using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {


	public float speed;
	public float jumpSpeed;
	public float gravity;
	private float yVelocity;
	private float rotation;
	private float posX;
	public float turnSpeed;
	
	public KeyCode run;

	private GameObject gameController;
	private bool eatenDot=false;
	
	private CharacterController cController;
	private Animator animator;
	
	// Use this for initialization
	void Start () {
	  cController = GetComponent<CharacterController>();
	  gameController = GameObject.FindWithTag("GameController");
	  animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	  
	}
	
	void FixedUpdate ()
	{
	  
	//  if (cController.isGrounded)
	 // {
	  //    yVelocity = 0;
	 //     if (Input.GetButtonDown("Jump"))
	//	  yVelocity = jumpSpeed;
	 // }
	 // yVelocity -= gravity;
	  
	  float moveHorizontal = Input.GetAxis("Horizontal");
	  float moveVertical = Input.GetAxis("Vertical");
	  Vector3 movement = new Vector3(0.0f,0.0f,moveVertical) * speed;
	  movement = transform.TransformDirection(movement);
	  movement.y += yVelocity;
	  //cController.Move(movement * Time.deltaTime);
	  animator.SetFloat("speed",moveVertical*speed);
	  
	  if (Input.GetKey(run))
	  {
	    animator.SetBool("running",true);
	  }
	  else
	  {
	    animator.SetBool("running",false);
	  }
	  
	  //transform.position = new Vector3(transform.position.x,0.31f,transform.position.z);
	  
	  //posX = Input.GetAxis("Mouse X");
	  Vector3 rot = transform.localEulerAngles;
	  rotation += moveHorizontal * turnSpeed;
	  rot.y = rotation;
	  transform.localEulerAngles = rot;
		if (eatenDot) {
			eatenDot=false;
			Debug.Log ("increase score");
			gameController.GetComponent<GameController> ().score++;
			gameController.GetComponent<GameController> ().pillsInWorld--;
			gameController.GetComponent<GameController> ().updateScore ();
						
		}

	}
	
	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Monster")
		{
			Debug.Log ("Hit a ghost");
			gameController.GetComponent<GameController>().levelFailed();
		}
	  if (other.gameObject.tag == "Pill")
	  {
	      Debug.Log ("eat pill "+other.gameObject.GetComponent<Pill>().index);
	      
	      other.gameObject.transform.localScale = new Vector3(0.15f,0.15f,0.15f);

	  if (!other.gameObject.GetComponent<Pill>().collected)
	  {
	      eatenDot=true;
	      other.gameObject.GetComponent<Pill>().collected = true;
	      other.gameObject.GetComponent<Pill>().transform.Translate(new Vector3(0,0.95f,0));
	      //other.gameObject.SetActive(false);
	      other.gameObject.GetComponent<Pill>().light.SetActive(true);
	      //Destroy (other.gameObject);
	  }
		   		

	  }
	

	
	
	
}
}
