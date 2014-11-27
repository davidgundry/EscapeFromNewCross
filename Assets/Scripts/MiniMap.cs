using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject player;
	public GameObject mainMap;
	private MazeManager mazeManager;
	private bool visible;
	public int cellHeight;
	public int cellWidth;
	private float halfCellWidth, halfCellHeight;
	// Use this for initialization
	void Start () {
		mazeManager = mainMap.GetComponent<MazeManager> ();
		visible = false;
		halfCellWidth = cellWidth / 2.0f;
		halfCellHeight = cellHeight / 2.0f;
	}
	public void setVisible(bool isVisible) {
				visible = isVisible;
		}
	
	// Update is called once per frame
	void Update () {
		if (visible) {
						Vector3 playerPos = player.transform.position;

						Vector2 cellPos = new Vector2 (Mathf.Round (playerPos.x + (mazeManager.width / 2) - 0.5f), Mathf.Round (playerPos.z + (mazeManager.height / 2) - 0.5f));

						//Debug.Log ("The player is at " + player.transform.position.ToString () + "cell=" + cellPos);
						if (!mazeManager.currentMaze.hasVisited ((int)cellPos.x, (int)cellPos.y)) {
								Debug.Log ("Draw cell at " + cellPos.ToString ());
								drawCell ((int) cellPos.x,(int)cellPos.y);
								mazeManager.currentMaze.setVisited ((int)cellPos.x, (int)cellPos.y, true);
						}
						
				}
	}
	void drawCell(int x, int y) {
	// offsets for each side of the cell
	Vector3 wallSize = wallPrefab.renderer.bounds.size;
	Vector3 Npos = new Vector3 (0, 0, -halfCellHeight + wallSize.z / 2);
	Vector3 Spos = new Vector3 (0, 0, halfCellHeight - wallSize.z / 2);
	Vector3 Wpos = new Vector3 (-halfCellWidth + wallSize.z / 2, 0, 0);
	Vector3 Epos = new Vector3 (halfCellWidth - wallSize.z / 2, 0, 0);
	if (mazeManager.currentMaze.hasDirection(x,y,Directions.N)) {
		makeWall (x, y, Npos, Vector3.zero);
	}
		if (mazeManager.currentMaze.hasDirection(x,y,Directions.S)) {
		makeWall (x, y, Spos, Vector3.zero);
	}
		if (mazeManager.currentMaze.hasDirection(x,y,Directions.W)) {
		makeWall (x, y, Wpos, new Vector3 (0, 90f, 0));
	}
		if (mazeManager.currentMaze.hasDirection(x,y,Directions.E)) {
		makeWall (x, y, Epos, new Vector3 (0, 90f, 0));
	}
}


// Adds the wall prefab to the scene
void makeWall(int x, int y,Vector3 offset,Vector3 rotate) {
	Vector3 position = new Vector3 (cellWidth * (0.5f+x-(mazeManager.width/2.0f)), 0, cellHeight * (0.5f+y-(mazeManager.height/2.0f))) + transform.position;
	GameObject newWall = (GameObject)Instantiate (wallPrefab, position+offset, Quaternion.Euler (rotate));
	newWall.transform.parent = transform;
}
}
