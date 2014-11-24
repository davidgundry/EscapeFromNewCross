using UnityEngine;
using System.Collections;

enum Direction {
	up,
	down,
	left,
	right
}

public class GhostScript : MonoBehaviour {

	private Direction direction;
	private Vector2[] directionVectors = new Vector2[] { new Vector2(0,1),new Vector2(0,-1),new Vector2(-1,0),new Vector2(1,0)};
	private Vector3 movement;
	public float speed;
	private bool moveBack;
	//private Vector2 backVector;


	// Use this for initialization
	void Start () {
		setDirection(Direction.down);
		moveBack = false;
	}
	

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "wall")
		{
			moveBack = true;
			Debug.Log ("Hit wall "+direction);


		}
	}
	void OnCollisionExit(Collision collision)
	{
		Debug.Log ("Not Hitting wall "+direction);
		moveBack = false;
		//setDirection(direction-1);
		setDirection((Direction)Random.Range ((int)Direction.up,(int)Direction.right));
	}
	void setDirection(Direction newDirection) {
		direction = newDirection;
		if (direction<0) {
			direction=Direction.right;
		}
		if ((direction==Direction.up) || (direction==Direction.down)) {
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
		} else {
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		}
	}

	void FixedUpdate () {

		Vector2 moveVector = directionVectors[(int)direction];
		movement = new Vector3(moveVector.x, 0.0f, moveVector.y);
		//Vector3 movement = new Vector3(-1f, 0.0f, 0f);
		//rigidbody.Move(movement* speed * Time.deltaTime);
		if (moveBack) {
				rigidbody.AddForce (movement * speed * Time.deltaTime);
		} else {
				rigidbody.AddForce (-movement * speed * Time.deltaTime);
		}

	}
}
