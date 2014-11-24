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
	private Vector2[] directionVectors = new Vector2[] { new Vector2(0,-1),new Vector2(0,1),new Vector2(-1,0),new Vector2(1,0)};
	public float speed;


	// Use this for initialization
	void Start () {
		direction = Direction.down;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "wall") {
			direction++;
			if (direction>Direction.right) {
				direction=0;

			}
		}
	}
	void FixedUpdate () {

		Vector2 moveVector = directionVectors[(int)direction];
		Vector3 movement = new Vector3(moveVector.x, 0.0f, moveVector.y);
		//Vector3 movement = new Vector3(-1f, 0.0f, 0f);
		rigidbody.MovePosition(movement* speed * Time.deltaTime);
		//rigidbody.AddForce(movement * speed * Time.deltaTime);
	}
}
