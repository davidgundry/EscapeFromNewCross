using UnityEngine;
using System.Collections;



public class MazeManager : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject dotPrefab;
	public int width;
	public int height;
	public int cellHeight;
	public int cellWidth;
	private float halfCellWidth, halfCellHeight;
	public Maze currentMaze;
	private MazeBuilder builder;
	private Floor mazeFloor;
	//private int[,] cells = new int[,]{{9,3,1,5},{8,8,0,4},{8,0,6,4},{10,2,2,6}};
	// Use this for initialization
	void Start () {
		mazeFloor = (Floor)GameObject.Find ("Floor").GetComponent (typeof(Floor));
		halfCellWidth = cellWidth / 2.0f;
		halfCellHeight = cellHeight / 2.0f;
		builder = new MazeBuilder ();
		createNewMaze (1);

	}
	public int createNewMaze(int newLevel) {
		width = getMazeSize (newLevel);
		height = width;
		removeCurrentMaze ();
		currentMaze = builder.Generate (width, height);
		drawMaze (currentMaze);
		mazeFloor.setSize (width, height);
		createDots ();
		return width * height;
	}
	void removeCurrentMaze() {
		GameObject[] allWalls;
		allWalls=GameObject.FindGameObjectsWithTag("wall");
		foreach (GameObject wall in allWalls) {
			Destroy (wall);
		}
	}
	int getMazeSize(int level) {
		return Mathf.Clamp (level+3, 4, 10);
	}
	void createDots() {
		Vector3 offset = new Vector3 (halfCellWidth, 0, halfCellWidth);
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject newPill=makePill (x,y,offset,Vector3.zero);
				newPill.GetComponent<Pill>().index= x*height+y;
			}
		}
	}
	
	void drawMaze(Maze drawMaze) {
				Vector3 wallSize = wallPrefab.renderer.bounds.size;
				Debug.Log ("wall size =" + wallSize);
				Vector3 Npos = new Vector3 (0, 0, -halfCellHeight + wallSize.z / 2);
				Vector3 Spos = new Vector3 (0, 0, halfCellHeight - wallSize.z / 2);
				Vector3 Wpos = new Vector3 (-halfCellWidth + wallSize.z / 2, 0, 0);
				Vector3 Epos = new Vector3 (halfCellWidth - wallSize.z / 2, 0, 0);
				for (int x = 0; x < width; x++) {
						for (int y = 0; y < height; y++) {
								if (drawMaze.hasDirection(x,y,Directions.N)) {
										makeWall (x, y, Npos, Vector3.zero);
								}
								if (drawMaze.hasDirection(x,y,Directions.S)) {
										makeWall (x, y, Spos, Vector3.zero);
								}
								if (drawMaze.hasDirection(x,y,Directions.W)) {
										makeWall (x, y, Wpos, new Vector3 (0, 90f, 0));
								}
								if (drawMaze.hasDirection(x,y,Directions.E)) {
										makeWall (x, y, Epos, new Vector3 (0, 90f, 0));
								}
						}
				}
		}


	void makeWall(int x, int y,Vector3 offset,Vector3 rotate) {
		Vector3 position = new Vector3 (cellWidth * (0.5f+x-(width/2.0f)), 0, cellHeight * (0.5f+y-(height/2.0f))) + transform.position;
				GameObject newWall = (GameObject)Instantiate (wallPrefab, position+offset, Quaternion.Euler (rotate));
				newWall.transform.parent = transform;
		}
	GameObject makePill(int x, int y,Vector3 offset,Vector3 rotate) {
		Vector3 position = new Vector3 (cellWidth * (x-(width/2.0f)), 0, cellHeight * (y-(height/2.0f))) + transform.position;
		GameObject newDot = (GameObject)Instantiate (dotPrefab, position+offset, Quaternion.Euler (rotate));
		newDot.transform.parent = transform;
		return newDot;
	}

	

}
