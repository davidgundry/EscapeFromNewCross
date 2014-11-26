using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostBillboard : MonoBehaviour {

	private Camera cam;
	
	public float speed;
	
	private Stack<Vector3> moveStack;
	private Maze maze;
	
	// Use this for initialization
	void Start () {
	  cam = Camera.main;
	  moveStack = new Stack<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
	  transform.LookAt(transform.position + cam.transform.rotation * new Vector3(0.0f,0.0f,1.0f),cam.transform.rotation * Vector3.up);
	}
	
	void pushPositionToStack(float x, float z)
	{
	  moveStack.Push(new Vector3(x-(maze.width/2.0f),0.5f,z-(maze.height/2.0f)));
	}
	
	void onEmptyStack()
	{
	  if (maze == null)
	    maze = GameObject.Find("MazeDrawer").GetComponent<MazeManager>().currentMaze;
	  int x = Random.Range(0,maze.width-1);
	  int z = Random.Range(0,maze.height-1);
	  pushPositionToStack(x+0.5f,z+0.5f);
	}
	
	void FixedUpdate()
	{
	  if (moveStack.Count>0)
	  {
	    Vector3 target = moveStack.Peek();
	    
	    Vector3 direction = target - transform.position;
	    float distanceToTarget = direction.magnitude;
	    direction.Normalize();
	    float moveAmount = Mathf.Clamp(distanceToTarget,0,speed*Time.deltaTime);
	 
	    transform.Translate(direction*moveAmount,Space.World);
	    
	    if (moveAmount < 0.001)
	      moveStack.Pop();
	  }
	  else
	  {
	    onEmptyStack();
	  }
	    
	}
}