using UnityEngine;
using System.Collections;

public class Pill : MonoBehaviour {


	private Camera cam;
	public bool collected=false;

	// Use this for initialization
	void Start () {
	  cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	  transform.LookAt(transform.position + cam.transform.rotation * new Vector3(0.0f,0.0f,1.0f),cam.transform.rotation * Vector3.up);
	}
}
