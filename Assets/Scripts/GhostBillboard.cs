using UnityEngine;
using System.Collections;

public class GhostBillboard : MonoBehaviour {

	private Camera cam;

	public int targetX;
	public float targetY = 0.5f;
	public int targetZ;
	
	public float speed;
	
	// Use this for initialization
	void Start () {
	  cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	  transform.LookAt(transform.position + cam.transform.rotation * new Vector3(0.0f,0.0f,1.0f),cam.transform.rotation * Vector3.up);
	}
	
	void FixedUpdate()
	{
	  Vector3 target = transform.InverseTransformPoint(targetX,targetY,targetZ);
	  
	  Vector3 direction = target - transform.position;
	  direction.Normalize();
	  
	  float distanceToTarget = Vector3.Distance( new Vector3(transform.position.x,0.0f,transform.position.z), new Vector3(targetX,0.0f,targetZ));
	  
	  transform.Translate(direction*Mathf.Clamp(speed,0,distanceToTarget)*Time.deltaTime);
	    
	}
}