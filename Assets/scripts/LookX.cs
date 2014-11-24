using UnityEngine;
using System.Collections;

public class LookX : MonoBehaviour {

	public float posX;
	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
	  posX = Input.GetAxis("Mouse X");
	  Vector3 rot = transform.localEulerAngles;
	  rot.y += posX * speed;
	  transform.localEulerAngles = rot;
	}
}
