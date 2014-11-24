using UnityEngine;
using System.Collections;

public class LookY : MonoBehaviour {

	public float posY;
	public float speed;
	public float max;
	public float min;
	public float rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
	  posY = Input.GetAxis("Mouse Y");
	  Vector3 rot = transform.localEulerAngles;
	  rotation += posY * speed;
	  rotation = Mathf.Clamp (rotation, min, max);
	  rot.x = rotation;
	  transform.localEulerAngles = rot;
	}
}
