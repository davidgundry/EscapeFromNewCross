using UnityEngine;
using System.Collections;

public class LookX : MonoBehaviour {

	public float posX;
	public float speed;
	public float max;
	public float min;
	private float rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
	  posX = Input.GetAxis("Mouse X");
	  Vector3 rot = transform.localEulerAngles;
	  rotation += posX * speed;
	  rotation = Mathf.Clamp (rotation, min, max);
	  rot.y = rotation;
	  transform.localEulerAngles = rot;
	}
}
