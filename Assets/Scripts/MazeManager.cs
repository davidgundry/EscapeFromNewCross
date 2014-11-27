using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MazeManager : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject dotPrefab;
	public GameObject ghostPrefab;
	public GameObject floorTilePrefab;
	public int width;
	public int height;
	public int cellHeight;
	public int cellWidth;
	private float halfCellWidth, halfCellHeight;
	public Maze currentMaze;
	private MazeBuilder builder;
	private Floor mazeFloor;
	private List<GameObject> ghosts;
	//private int[,] cells = new int[,]{{9,3,1,5},{8,8,0,4},{8,0,6,4},{10,2,2,6}};
	// Use this for initialization
	void Start () {
		mazeFloor = (Floor)GameObject.Find ("Floor").GetComponent (typeof(Floor));
		halfCellWidth = cellWidth / 2.0f;
		halfCellHeight = cellHeight / 2.0f;
		builder = new MazeBuilder ();
		//createNewMaze (1);

	}
	public int createNewMaze(int newLevel) {
		Debug.Log ("creating new maze " + newLevel);
		width = getMazeSize (newLevel);
		height = width;
		removeCurrentMaze ();
		// generates maze shape
		currentMaze = builder.Generate (width, height);
		// adds maze prefabs to the scene
		drawMaze (currentMaze);
		mazeFloor.setSize (width, height);
		createDots ();
		createGhosts (newLevel);
		return width * height;
	}
	public List<GameObject> getGhosts() {
				return ghosts;
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
	void createGhosts(int newLevel) {
		ghosts = new List<GameObject>();

				int numberOfGhosts = newLevel;
				do {
						Vector3 offset = new Vector3 (halfCellWidth, 0, halfCellWidth);
						ghosts.Add(makeGhost (offset, Vector3.zero));

						numberOfGhosts--;
				} while (numberOfGhosts>0);
		}

	// Goes through the Maze data structure and creates prefab blocks in the right places and orientation
	void drawMaze(Maze drawMaze) {
				Vector3 wallSize = wallPrefab.renderer.bounds.size;
				Debug.Log ("wall size =" + wallSize);
				// offsets for each side of the cell
				Vector3 Npos = new Vector3 (0, 0, -halfCellHeight + wallSize.z / 2);
				Vector3 Spos = new Vector3 (0, 0, halfCellHeight - wallSize.z / 2);
				Vector3 Wpos = new Vector3 (-halfCellWidth + wallSize.z / 2, 0, 0);
				Vector3 Epos = new Vector3 (halfCellWidth - wallSize.z / 2, 0, 0);
				for (int x = 0; x < width; x++) {
						for (int y = 0; y < height; y++) {
								makeFloor(x,y);
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

	void makeFloor(int x, int y)
	{
		Vector3 offset = Vector3.zero;
		Vector3 rotate = Vector3.zero;
		Vector3 position = new Vector3 (cellWidth * (0.5f+x-(width/2.0f)), -0.5f, cellHeight * (0.5f+y-(height/2.0f))) + transform.position;
		GameObject newFloor = (GameObject)Instantiate (floorTilePrefab, position+offset, Quaternion.Euler (rotate));
		GameObject newCeiling = (GameObject)Instantiate (floorTilePrefab, position+offset+new Vector3(0,1.5f,0), Quaternion.Euler (rotate));
		newFloor.transform.parent = transform;
		newCeiling.transform.parent = transform;
	}
		
	// Adds the wall prefab to the scene
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
		GameObject makeGhost(Vector3 offset,Vector3 rotate) {
		Vector2 pos;
			Vector2 player = new Vector2 (0, 0);
		do {
				pos = new Vector2 (Random.Range (0, width - 1), Random.Range (0, height - 1));
		} while (Vector2.Distance(pos,player)<2);

			Vector3 position = new Vector3 (cellWidth * (pos.x-(width/2.0f)), 0, cellHeight * (pos.y-(height/2.0f))) + transform.position;
			GameObject newGhost = (GameObject)Instantiate (ghostPrefab, position+offset, Quaternion.Euler (rotate));
			newGhost.transform.parent = transform;
			return newGhost;
		}

	

}
