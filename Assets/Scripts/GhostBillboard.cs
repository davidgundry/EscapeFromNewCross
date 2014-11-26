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
	
	void pushTargetToStack(Vector3 target)
	{
	  moveStack.Push(target);
	}
	
	int cellX()
	{
	  return (int) Mathf.Floor(transform.position.x + maze.width/2);
	}
	
	int cellY()
	{
	  return (int) Mathf.Floor(transform.position.z + maze.height/2);
	}
	
	Vector3 worldPositionOfCell(int x, int y)
	{
	  return new Vector3(x-(maze.width/2)+0.5f,0.5f,y-(maze.width/2)+0.5f);
	}
	
	bool moveInDirection()
	{
	  bool cantMove = true;
	  if (!maze.hasDirection(cellX(),cellY(),direction))
	  {
	    if (direction == Directions.N)
	    {
	      if (!maze.hasDirection(cellX(),cellY()-1,Directions.S))
	      {
		pushTargetToStack(worldPositionOfCell(cellX(),cellY()-1));
		cantMove = false;
	      }
	    }
	    else if (direction == Directions.S)
	    {
	      if (!maze.hasDirection(cellX(),cellY()+1,Directions.N))
	      {
		pushTargetToStack(worldPositionOfCell(cellX(),cellY()+1));
		cantMove = false;
	      }
	    }
	    else if (direction == Directions.E)
	    {
	      if (!maze.hasDirection(cellX()+1,cellY(),Directions.W))
	      {
		pushTargetToStack(worldPositionOfCell(cellX()+1,cellY()));
		cantMove = false;
	      }
	    }
	    else if (direction == Directions.W)
	    {
	      if (!maze.hasDirection(cellX()-1,cellY(),Directions.E))
	      {
		pushTargetToStack(worldPositionOfCell(cellX()-1,cellY()));
		cantMove = false;
	      }
	    }
	  }
	  return !cantMove;
	}
	
	void moveRandomOnCollide()
	{
	  if (!moveInDirection())
	  {
	    randomiseDirection();
	  }
	}
	
	void randomiseDirection()
	{
	  int d = (int) Mathf.Floor(Random.Range(0,3.9f));
	  if (d == 0)
	    direction = Directions.N;
	  else if (d == 1)
	    direction = Directions.E;
	  else if (d == 2)
	    direction = Directions.S;
	  else if (d == 3)
	    direction = Directions.W;
	}
	
	void moveRandomly()
	{
	  randomiseDirection();
	  moveInDirection();
	}
	
	void onEmptyStack()
	{
	  if (maze == null)
	  {
	    maze = GameObject.Find("MazeDrawer").GetComponent<MazeManager>().currentMaze;
	    direction = Directions.N;
	  }
	  
	  //moveRandomOnCollide();
	  moveRandomly();
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