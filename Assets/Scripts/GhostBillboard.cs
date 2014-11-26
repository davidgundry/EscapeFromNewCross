using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostBillboard : MonoBehaviour {

	private Camera cam;
	
	public float speed;
	
	private Stack<Vector3> moveStack;
	private Maze maze;
	
	private Directions direction;
	
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
	
	int cellX()
	{
	  return (int) transform.position.x + maze.width/2;
	}
	
	int cellY()
	{
	  return (int) transform.position.z + maze.height/2;
	}
	
	void onEmptyStack()
	{
	  if (maze == null)
	  {
	    maze = GameObject.Find("MazeDrawer").GetComponent<MazeManager>().currentMaze;
	    direction = Directions.N;
	  }
	  
	  Debug.Log("x:"+cellX()+" y:"+cellY());
	  
	  bool cantMove = true;
	  if (!maze.hasDirection(cellX(),cellY(),direction))
	  {
	    if (direction == Directions.N)
	    {
	      if (!maze.hasDirection(cellX(),cellY()+1,Directions.S))
	      {
		pushPositionToStack(cellX()+0.5f,cellY()+1.5f);
		cantMove = false;
	      }
	    }
	    else if (direction == Directions.S)
	    {
	      if (!maze.hasDirection(cellX(),cellY()-1,Directions.N))
	      {
		pushPositionToStack(cellX()+0.5f,cellY()-0.5f);
		cantMove = false;
	      }
	    }
	    else if (direction == Directions.E)
	    {
	      if (!maze.hasDirection(cellX()+1,cellY(),Directions.W))
	      {
		pushPositionToStack(cellX()+1.5f,cellY()+0.5f);
		cantMove = false;
	      }
	    }
	    else if (direction == Directions.W)
	    {
	      if (!maze.hasDirection(cellX()-1,cellY(),Directions.E))
	      {
		pushPositionToStack(cellX()-0.5f,cellY()+0.5f);
		cantMove = false;
	      }
	    }
	  }
	  
	  if (cantMove)
	  {
	    int d = Random.Range(0,3);
	    if (d == 0)
	      direction = Directions.N;
	    else if (d == 1)
	      direction = Directions.E;
	    else if (d == 2)
	      direction = Directions.S;
	    else if (d == 3)
	      direction = Directions.W;
	  }
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