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
	
	public GameObject lookY;

	public Vector3 rotationToApply;
	
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

	  
	  
	  RaycastHit hit;
	  if( Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
	    distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
	  else
	    distance = maxDistance;
	    
	  transform.localPosition = cameraDirection * distance;
	    
	  transform.Rotate(rotationToApply);
	  rotationToApply = new Vector3(0,0,0);
	  if (lookY.transform.rotation.eulerAngles.x > 12.0f)
	    rotationToApply = new Vector3(-1.0f,0,0);
	}
	
	void OnTriggerEnter()
	{
	  if (lookY.transform.rotation.eulerAngles.x < 70.0f)
	    rotationToApply += new Vector3(10.0f,0,0);
	  Debug.Log("Camera Collision");
	}
}
