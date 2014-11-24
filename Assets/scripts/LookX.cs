using UnityEngine;
using System.Collections;

public class LookX : MonoBehaviour {

	public float posX;
	public float speed;
	public float max;
	public float min;
	private float rotation;
	
	public float minDistance;
	public float maxDistance;
	public float distance;
	public Vector3 cameraDirection;

	void Awake()
	{
	  distance = transform.localPosition.magnitude;
	  cameraDirection = transform.localPosition.normalized;
	}
	
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
	/*  posX = Input.GetAxis("Mouse X");
	  Vector3 rot = transform.localEulerAngles;
	  rotation += posX * speed;
	  rotation = Mathf.Clamp (rotation, min, max);
	  rot.y = rotation;
	  transform.localEulerAngles = rot;*/
	  
	  cameraDirection = transform.localPosition.normalized;
	  Vector3 desiredCameraPos = transform.parent.TransformPoint(cameraDirection * maxDistance);

	  distance = maxDistance;
	  
	  RaycastHit hit;
	  if( Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
	  {
	      distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
	  }

	  transform.localPosition = cameraDirection * distance;
	}
}
